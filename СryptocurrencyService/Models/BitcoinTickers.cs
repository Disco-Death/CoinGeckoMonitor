using Newtonsoft.Json;

namespace СryptocurrencyService.Models;

public class BitcoinTickers
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Base { get; set; } = null!;
    public string Target { get; set; } = null!;
    public double Last { get; set; } = 0;
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;
    public string TradeUrl { get; set; } = null!;
}