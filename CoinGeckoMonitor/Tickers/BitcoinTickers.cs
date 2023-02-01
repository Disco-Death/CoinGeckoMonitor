using Newtonsoft.Json;

namespace CoinGeckoMonitor.Tickers;

public class BitcoinTickers
{
    [JsonProperty("Name")]
    public string Name { get; set; } = null!;

    [JsonProperty("Base")]
    public string Base { get; set; } = null!;

    [JsonProperty("Target")]
    public string Target { get; set; } = null!;

    [JsonProperty("Last")]
    public double Last { get; set; } = 0;

    [JsonProperty("Timestamp")]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

    [JsonProperty("TradeUrl")]
    public string TradeUrl { get; set; } = null!;
}