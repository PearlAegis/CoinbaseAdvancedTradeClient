using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.WebSocket;
using CoinbaseAdvancedTradeClient.Models.WebSocket.Events;
using CoinbaseAdvancedTradeClient.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Authentication;
using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient
{
    public class CoinbaseAdvancedTradeWebSocketClient : ICoinbaseAdvancedTradeWebSocketClient, IDisposable
    {
        private WebSocketClientConfig _config;
        private WebSocket _socket;

        public bool IsConnected => Socket?.State == WebSocketState.Open;
        public bool IsSubscribed { get; private set; } = false;

        private List<string> _productIds;
        private string _channel;
        private Action<object> _messageReceivedCallback;

        public CoinbaseAdvancedTradeWebSocketClient(WebSocketClientConfig config)
        {
            ValidateConfig(config);
        }

        private void ValidateConfig(WebSocketClientConfig? config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.ApiKey)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.ApiKey));
            if (string.IsNullOrWhiteSpace(config.ApiSecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.ApiSecret));

            _config = config;
        }

        public async Task<bool> ConnectAsync(List<string> productIds, string channel, Action<object> messageReceivedCallback)
        {
            if (productIds == null || !productIds.Any()) throw new ArgumentNullException(nameof(productIds), ErrorMessages.ProductIdRequired);
            if (string.IsNullOrWhiteSpace(channel) || !WebSocketChannels.WebSocketChannelList.Contains(channel)) throw new ArgumentNullException(nameof(channel), ErrorMessages.ChannelRequired);

            _productIds = productIds;
            _channel = channel;
            _messageReceivedCallback = messageReceivedCallback;

            if (_socket != null)
            {
                Disconnect();
            }

            _socket = new WebSocket(_config.WebSocketUrl);
            _socket.Security.EnabledSslProtocols = SslProtocols.Tls12;

            _socket.Opened += Socket_Opened;
            _socket.Closed += Socket_Closed;
            _socket.Error += Socket_Error;
            _socket.MessageReceived += Socket_MessageReceived;

            return await _socket.OpenAsync();
        }

        private void Socket_Opened(object sender, EventArgs e)
        {
            if (_socket != null)
        {
                if (_socket.State == WebSocketState.Open)
        {
                    Unsubscribe();
        }
            }

            _socket?.Close();
            _socket?.Dispose();
            _socket = null;
            }
        }

        private bool ParseWebSocketMessage(string json, out object websocketEvent)
        {
            var obj = JObject.Parse(json);

            var channel = obj["channel"].Value<string>();

            switch (channel)
            {
                case "market_trades":
                    websocketEvent = obj.ToObject<WebSocketMessage<MarketTradesEvent>>();
                    break;
                case "status":
                    websocketEvent = obj.ToObject<WebSocketMessage<StatusEvent>>();
                    break;
                case "ticker":
                case "ticker_batch":
                    websocketEvent = obj.ToObject<WebSocketMessage<TickerEvent>>();
                    break;
                case "l2_data":
                    websocketEvent = obj.ToObject<WebSocketMessage<Level2Event>>();
                    break;
                case "users":
                    websocketEvent = obj.ToObject<WebSocketMessage<UserEvent>>();
                    break;
                default:
                    websocketEvent = null;
                    return false;
            }

            return true;
        }

        public void Subscribe()
        {
            if (!IsConnected) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(Config.ApiSecret, timestamp, _channel, _productIds);

            var subscription = new Subscription
            {
                ApiKey = _config.ApiKey,
                Channel = _channel,
                ProductIds = _productIds,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Subscribe,
            };

            var subscribeMessage = JsonConvert.SerializeObject(subscription);

            _socket.Send(subscribeMessage);

            IsSubscribed = true;
        }

        public void Unsubscribe()
        {
            if (!IsConnected) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
            var signature = ApiKeyAuthenticator.GenerateWebSocketSignature(_config.ApiSecret, timestamp, _channel, _productIds);

            var unsubscribe = new Subscription
            {
                ApiKey = _config.ApiKey,
                Channel = _channel,
                ProductIds = _productIds,
                Signature = signature,
                Timestamp = timestamp,
                Type = SubscriptionTypes.Unsubscribe
            };

            var unsubscribeMessage = JsonConvert.SerializeObject(unsubscribe);

            _socket.Send(unsubscribeMessage);

            IsSubscribed = false;
        }

        public void Disconnect()
        {
            if (Socket != null)
            {
                if (Socket.State == WebSocketState.Open)
                {
                    Unsubscribe();
                }
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
