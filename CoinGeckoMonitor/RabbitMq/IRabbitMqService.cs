using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinGeckoMonitor.RabbitMq
{
    internal interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
