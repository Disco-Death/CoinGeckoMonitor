using CoinGeckoMonitor;
using CoinGeckoMonitor.Monitors;
using CoinGeckoMonitor.RabbitMq;
using Microsoft.Extensions.Configuration;

var builder = Config.GetInstance().Root;

using var mqService = new RabbitMqService(
    builder.GetConnectionString("RabbitMqConnection"),
    builder.GetConnectionString("QueueName"));

ICryptoMonitor monitor = new BitcoinMonitor(mqService);
monitor.Start();

Console.ReadKey();