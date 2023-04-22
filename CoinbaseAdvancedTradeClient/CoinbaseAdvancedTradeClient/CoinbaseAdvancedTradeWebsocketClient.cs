using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.WebSocket;
using CoinbaseAdvancedTradeClient.Resources;
using Newtonsoft.Json;
using System.Security.Authentication;
using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient
{
    public class CoinbaseAdvancedTradeWebSocketClient : ICoinbaseAdvancedTradeWebSocketClient, IDisposable
    {
        public WebSocketClientConfig Config { get; private set; }
        public WebSocket Socket { get; private set; }

        private List<string> _productIds;
        private string _channel;

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

        public async Task<bool> ConnectAsync(List<string> productIds, string channel)
        {
            if (productIds == null || !productIds.Any()) throw new ArgumentNullException(nameof(productIds), ErrorMessages.ProductIdRequired);
            if (string.IsNullOrWhiteSpace(channel) || !WebSocketChannels.WebSocketChannelList.Contains(channel)) throw new ArgumentNullException(nameof(channel), ErrorMessages.ChannelRequired);

            _productIds = productIds;
            _channel = channel;

            if (Socket != null)
            {
                Disconnect();
            }    

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

        public void Subscribe()
        {
            if (Socket?.State != WebSocketState.Open) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(Config.ApiSecret, timestamp, _channel, _productIds);

            var subscription = new Subscription
            {
                ApiKey = Config.ApiKey,
                Channel = _channel,
                ProductIds = _productIds,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Subscribe,
            };

            var subscribeMessage = JsonConvert.SerializeObject(subscription);

            Socket.Send(subscribeMessage);
        }

        public void Unsubscribe()
        {
            if (Socket?.State != WebSocketState.Open) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(Config.ApiSecret, timestamp, _channel, _productIds);

            var unsubscribe = new Subscription
            {
                ApiKey = Config.ApiKey,
                Channel = _channel,
                ProductIds = _productIds,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Unsubscribe
            };

            var unsubscribeMessage = JsonConvert.SerializeObject(unsubscribe);

            Socket.Send(unsubscribeMessage);
        }

        public void Disconnect()
        {
            if (Socket != null)
            {
                if (Socket.State == WebSocketState.Open)
                {
                    Unsubscribe();
                }

                Socket.Opened -= RawSocket_Opened;
                Socket.Error -= RawSocket_Error;
            }

            Socket?.Close();
            Socket?.Dispose();
            Socket = null;
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
