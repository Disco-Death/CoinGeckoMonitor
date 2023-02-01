using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace СryptocurrencyService.RabbitMq;

public class RabbitMqListener : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IReceiver _receiver;
    private readonly EventingBasicConsumer _consumer;
    private bool disposed = false;
    public RabbitMqListener(string hostName, string queueName, IReceiver receiver)
    {
        _connection = new ConnectionFactory { HostName = hostName }.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: queueName,
                 durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);
        _queueName = queueName;
        _receiver = receiver;

        _consumer = new EventingBasicConsumer(_channel);
        AddConsumerReceiver();
    }

    private void AddConsumerReceiver()
    {
        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            _receiver.Send(message);
        };

        _channel.BasicConsume(queue: _queueName,
                              autoAck: false,
                              consumer: _consumer);
        Console.WriteLine(" [*] Waiting for messages.");
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
            _channel.Close();
            _connection.Close();
        }
        disposed = true;
    }

    ~RabbitMqListener()
    {
        Dispose(false);
    }

}



