using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using СryptocurrencyService.RabbitMq;

namespace СryptocurrencyService.Receivers;

internal class DbBitcoinReceiver : IReceiver, IDisposable
{
    private readonly CryptodbContext _db;
    private bool disposed = false;
    public DbBitcoinReceiver(DbContextOptions<CryptodbContext> options)
    {
        _db = new CryptodbContext(options);
    }
    public void Send(string data)
    {
        var obj = JsonConvert.DeserializeObject<Tickers.BitcoinTickers>(data);

        _db.BitcoinTickers.Add(new Models.BitcoinTickers()
        {
            Name = obj.Name,
            Base = obj.Base,
            Target = obj.Target,
            Last = obj.Last,
            Timestamp = obj.Timestamp,
            TradeUrl = obj.TradeUrl
        });

        _db.SaveChanges();
        Console.WriteLine("Объект успешно сохранён");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        if (disposing)
        {
            _db.Dispose();
        }
        disposed = true;
    }

    ~DbBitcoinReceiver()
    {
        Dispose(false);
    }
}
