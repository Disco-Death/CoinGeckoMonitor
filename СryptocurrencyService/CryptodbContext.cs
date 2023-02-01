using Microsoft.EntityFrameworkCore;
using СryptocurrencyService.Models;

namespace СryptocurrencyService; 

public class CryptodbContext : DbContext
{
    public DbSet<BitcoinTickers> BitcoinTickers { get; set; } = null!;
    public CryptodbContext(DbContextOptions<CryptodbContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            message => System.Diagnostics.Debug.WriteLine(message),
            new[] { DbLoggerCategory.Database.Command.Name });
    }
}
