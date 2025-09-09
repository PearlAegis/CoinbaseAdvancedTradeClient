using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinbaseAdvancedTradeClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds CoinbaseAdvancedTradeClient services to the service collection.
        /// Automatically resolves configuration from the CoinbaseClientConfig section in appsettings.json.
        /// </summary>
        /// <param name="services">The service collection to add services to</param>
        /// <returns>The service collection for method chaining</returns>
        public static IServiceCollection AddCoinbaseAdvancedTradeClient(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddOptions<CoinbaseClientConfig>()
                .Configure<IConfiguration>((config, configuration) =>
                {
                    configuration.GetSection(nameof(CoinbaseClientConfig)).Bind(config);
                });

            services.AddScoped<ICoinbaseAdvancedTradeApiClient, CoinbaseAdvancedTradeApiClient>();
            services.AddScoped<ICoinbaseAdvancedTradeWebSocketClient, CoinbaseAdvancedTradeWebSocketClient>();

            return services;
        }
    }
}
