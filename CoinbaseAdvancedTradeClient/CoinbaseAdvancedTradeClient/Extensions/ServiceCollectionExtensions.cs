using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinbaseAdvancedTradeClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoinbaseAdvancedTradeClient(this IServiceCollection services, IConfiguration configurationManager, ServiceLifetime apiClientLifetime = ServiceLifetime.Singleton, ServiceLifetime webSocketLifetime = ServiceLifetime.Scoped)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configurationManager == null) throw new ArgumentNullException(nameof(configurationManager));

            services.Configure<CoinbaseClientConfig>(configurationManager.GetSection(nameof(CoinbaseClientConfig)));

            services.Add(new ServiceDescriptor(typeof(ICoinbaseAdvancedTradeApiClient), typeof(CoinbaseAdvancedTradeApiClient), apiClientLifetime));
            services.Add(new ServiceDescriptor(typeof(ICoinbaseAdvancedTradeWebSocketClient), typeof(CoinbaseAdvancedTradeWebSocketClient), apiClientLifetime));
        }
    }
}
