namespace СryptocurrencyService.RabbitMq;

public interface IReceiver
{
    void Send(string data);
}
