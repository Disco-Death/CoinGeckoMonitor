using CoinGecko.Clients;
using CoinGeckoMonitor.RabbitMq;
using CoinGeckoMonitor.Tickers;
using Newtonsoft.Json;
using System.Timers;

namespace CoinGeckoMonitor.Monitors;

public class BitcoinMonitor : ICryptoMonitor
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly PingClient _pingClient;
    private readonly CoinGeckoClient _client;
    private readonly IReceiver _receiver;
    private readonly Thread _monitorThread;
    private const string BITCOIN_ID = "bitcoin";

    public BitcoinMonitor(IReceiver receiver)
    {
        _httpClient = new();
        _serializerSettings = new();
        _pingClient = new PingClient(_httpClient, _serializerSettings);
        _client = new CoinGeckoClient(_httpClient, _serializerSettings);
        _monitorThread = new Thread(InitTimer);
        _receiver = receiver;
    }

    public void Start()
    {
        _monitorThread.Start();
    }

    private void InitTimer()
    {
        var timer = new System.Timers.Timer();
        timer.Elapsed += OnTimedEvent;
        timer.Enabled = true;
        timer.Interval = 5000;
        timer.AutoReset = true;
    }

    private async void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        // Check CoinGecko API status
        if ((await _pingClient.GetPingAsync()).GeckoSays != string.Empty)
        {
            var ticker = await _client.CoinsClient.GetTickerByCoinId(BITCOIN_ID);

            var message = JsonConvert.SerializeObject(new BitcoinTickers()
            {
                Name = ticker.Name,
                Base = ticker.Tickers[0].Base,
                Target = ticker.Tickers[0].Target,
                Last = (double)ticker.Tickers[0].Last,
                Timestamp = (DateTimeOffset)ticker.Tickers[0].Timestamp,
                TradeUrl = ticker.Tickers[0].TradeUrl
            });

            Console.WriteLine(message);

            _receiver.Send(message);
        }
    }
}
