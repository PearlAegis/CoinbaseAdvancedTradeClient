using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.WebSocket;
using CoinbaseAdvancedTradeClient.Resources;
using System.Net.WebSockets;

namespace CoinbaseAdvancedTradeClient
{
    public class CoinbaseAdvancedTradeWebSocketClient : ICoinbaseAdvancedTradeWebSocketClient
    {
        public WebSocketClientConfig Config { get; private set; }
        public WebSocket Socket { get; private set; }

        public CoinbaseAdvancedTradeWebSocketClient(WebSocketClientConfig config)
        {
            ValidateConfig(config);
        }

        private void ValidateConfig(WebSocketClientConfig? config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.ApiKey)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.ApiKey));
            if (string.IsNullOrWhiteSpace(config.ApiSecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.ApiSecret));

            Config = config;
        }

        public async Task ConnectAsync()
        {
            if (Socket != null) throw new InvalidOperationException("Socket already exists. TODO error message");

            Socket = new ClientWebSocket();
        }

        public async Task SubscribeAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
