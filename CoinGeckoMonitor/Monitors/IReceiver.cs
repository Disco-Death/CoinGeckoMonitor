namespace CoinGeckoMonitor.Monitors
{
    public interface IReceiver
    {
        void Send(string data);
    }
}