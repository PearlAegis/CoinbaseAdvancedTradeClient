using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.WebSocket;
using CoinbaseAdvancedTradeClient.Resources;
using Newtonsoft.Json;
using System.Security.Authentication;
using System.Threading.Channels;
using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient
{
    public class CoinbaseAdvancedTradeWebSocketClient : ICoinbaseAdvancedTradeWebSocketClient, IDisposable
    {
        public WebSocketClientConfig Config { get; private set; }
        public WebSocket Socket { get; private set; }

        private TaskCompletionSource<ConnectResult> _connectionCompletionSource;

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

        public async Task<bool> ConnectAsync()
        {
            if (Socket != null) throw new InvalidOperationException("Socket already exists. TODO error message");

            Socket = new WebSocket(Config.WebSocketUrl);
            Socket.Security.EnabledSslProtocols = SslProtocols.Tls12;

            Socket.Opened += RawSocket_Opened;
            Socket.Error += RawSocket_Error;

            return await Socket.OpenAsync();
        }

        private void RawSocket_Opened(object sender, EventArgs e)
        {
        }

        private void RawSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
        }

        public async Task SubscribeAsync(List<string> productIds, string channel)
        {
            if (productIds == null || !productIds.Any()) throw new ArgumentException("ProductID required TODO");
            if (string.IsNullOrWhiteSpace(channel) || !WebSocketChannels.WebSocketChannelList.Contains(channel)) throw new ArgumentNullException("Channel required TODO");

            if (Socket?.State != WebSocketState.Open) throw new InvalidOperationException("Socket must be connected.");

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(Config.ApiSecret, timestamp, channel, productIds);

            var subscription = new Subscription
            {
                ApiKey = Config.ApiKey,
                Channel = channel,
                ProductIds = productIds,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Subscribe.ToString().ToLowerInvariant(),
            };

            var subscribeMessage = JsonConvert.SerializeObject(subscription);

            Socket.Send(subscribeMessage);
        }

        public void Unsubscribe()
        {

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(Config.ApiSecret, timestamp, channel, productIds);

            var unsubscribe = new Subscription
            {
                ApiKey = Config.ApiKey,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Unsubscribe.ToString().ToLowerInvariant()
            };

            var unsubscribeMessage = JsonConvert.SerializeObject(unsubscribe);

            Socket.Send(unsubscribeMessage);
        }

        public void Dispose()
        {
            Socket.Dispose();
            Socket = null;
        }
    }
}
