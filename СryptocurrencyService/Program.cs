using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using СryptocurrencyService;
using СryptocurrencyService.RabbitMq;
using СryptocurrencyService.Receivers;

var config = Config.GetInstance().Root;

var options = new DbContextOptionsBuilder<CryptodbContext>()
    .UseSqlServer(config.GetConnectionString("MssqlConnection"))
    .Options;

using var receiver = new DbBitcoinReceiver(options);
using var listener = new RabbitMqListener(
    config.GetConnectionString("RabbitMqConnection"),
    config.GetConnectionString("QueueName"),
    receiver);

Console.ReadKey();