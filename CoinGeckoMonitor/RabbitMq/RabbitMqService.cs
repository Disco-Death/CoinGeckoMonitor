using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using CoinGeckoMonitor.Monitors;

namespace CoinGeckoMonitor.RabbitMq
{
    internal class RabbitMqService : IRabbitMqService, IReceiver, IDisposable
	{
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private bool disposed = false;
        public RabbitMqService(string hostName, string queueName)
        {
            _connection = new ConnectionFactory() { HostName = hostName }
                .CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);
            _queueName = queueName;
        }

		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}

		public void SendMessage(string message)
		{
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueName,
                basicProperties: null,
                body: body);
        }

        public void Send(string message)
        {
            SendMessage(message);
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

        ~RabbitMqService()
        {
            Dispose(false);
        }
	}
}
