using Microsoft.Extensions.Configuration;

namespace СryptocurrencyService
{
    public class Config
    {
        private static Config instance = null!;
        public IConfigurationRoot Root { get; private set; } = null!;
        private static readonly object syncRoot = new();

        protected Config()
        {
            Root = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static Config GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Config();
                }
            }
            return instance;
        }
    }
}
