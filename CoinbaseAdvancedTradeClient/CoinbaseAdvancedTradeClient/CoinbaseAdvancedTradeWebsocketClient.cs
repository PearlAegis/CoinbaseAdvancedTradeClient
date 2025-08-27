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
        private readonly SecretApiKeyWebSocketConfig _config;
        private WebSocket _socket;

        private Action<object?, bool> _messageReceivedCallback;
        private Action<object?, EventArgs>? _openedCallback;
        private Action<object?, EventArgs>? _closedCallback;
        private Action<Exception>? _errorCallback;

        public bool IsConnected => _socket?.State == WebSocketState.Open;

        public CoinbaseAdvancedTradeWebSocketClient(SecretApiKeyWebSocketConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.KeyName)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.KeyName));
            if (string.IsNullOrWhiteSpace(config.KeySecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.KeySecret));

            _config = config;
        }

        #region Connection

        public async Task<bool> ConnectAsync(Action<object?, bool> messageReceivedCallback, Action<object?, EventArgs>? openedCallback = null, Action<object?, EventArgs>? closedCallback = null, Action<Exception>? errorCallback = null)
        {
            if (messageReceivedCallback == null) throw new ArgumentNullException(nameof(messageReceivedCallback), ErrorMessages.MessageReceivedCallbackRequired);

            _messageReceivedCallback = messageReceivedCallback;
            _openedCallback = openedCallback;
            _closedCallback = closedCallback;
            _errorCallback = errorCallback;

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

        public void Disconnect()
        {
            if (_socket != null && _socket.State != WebSocketState.Closed && _socket.State != WebSocketState.Closing)
            {
                _socket.Close();
            }
        }

        #endregion // Connection

        #region Subscription

        public void Subscribe(string channel, List<string> productIds)
        {
            if (string.IsNullOrWhiteSpace(channel) || !WebSocketChannels.WebSocketChannelList.Contains(channel)) throw new ArgumentException(ErrorMessages.ChannelRequired, nameof(channel));
            if (productIds == null || !productIds.Any()) throw new ArgumentNullException(nameof(productIds), ErrorMessages.ProductIdRequired);

            if (!IsConnected) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            // Generate JWT for WebSocket subscription
            var uri = new Uri(_config.WebSocketUrl);
            var jwt = SecretApiKeyAuthenticator.GenerateBearerJWT(
                _config.KeyName,
                _config.KeySecret,
                "GET",
                uri.Host,
                uri.PathAndQuery);

            var subscriptionMessage = new SubscriptionMessage
            {
                Type = SubscriptionType.Subscribe,
                Channel = channel,
                ProductIds = productIds,
                Jwt = jwt
            };

            var subscribe = JsonConvert.SerializeObject(subscriptionMessage);

            _socket.Send(subscribe);
        }

        public void Unsubscribe(string channel, List<string> productIds)
        {
            if (string.IsNullOrWhiteSpace(channel) || !WebSocketChannels.WebSocketChannelList.Contains(channel)) throw new ArgumentException(ErrorMessages.ChannelRequired, nameof(channel));
            if (productIds == null || !productIds.Any()) throw new ArgumentNullException(nameof(productIds), ErrorMessages.ProductIdRequired);

            if (!IsConnected) throw new InvalidOperationException(ErrorMessages.WebSocketMustBeConnected);

            // Generate JWT for WebSocket unsubscription
            var uri = new Uri(_config.WebSocketUrl);
            var jwt = SecretApiKeyAuthenticator.GenerateBearerJWT(
                _config.KeyName,
                _config.KeySecret,
                "GET",
                uri.Host,
                uri.PathAndQuery);

            var unsubscribeMessage = new SubscriptionMessage
            {
                Type = SubscriptionType.Unsubscribe,
                Channel = channel,
                ProductIds = productIds,
                Jwt = jwt
            };

            var unsubscribe = JsonConvert.SerializeObject(unsubscribeMessage);

            _socket.Send(unsubscribe);
        }

        #endregion // Subscription

        #region Event Handlers

        private void Socket_Opened(object? sender, EventArgs e)
        {
            if (_openedCallback != null)
            {
                _openedCallback.Invoke(sender, e);
            }
        }

        private void Socket_Closed(object? sender, EventArgs e)
        {
            if (_closedCallback != null)
            {
                _closedCallback.Invoke(sender, e);
            }
        }

        private void Socket_Error(object? sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (_errorCallback != null)
            {
                _errorCallback.Invoke(e.Exception);
            }
            else
            {
                throw e.Exception;
            }
        }

        private void Socket_MessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            var parsed = ParseWebSocketMessage(e.Message, out object message);

            _messageReceivedCallback.Invoke(message, parsed);
        }

        private bool ParseWebSocketMessage(string json, out object message)
        {
            var obj = JObject.Parse(json);

            var channel = obj[WebSocketChannels.Channel]?.Value<string>();

            switch (channel)
            {
                case WebSocketChannels.MarketTrades:
                    message = obj.ToObject<WebSocketMessage<MarketTradesEvent>>();
                    return true;
                case WebSocketChannels.Status:
                    message = obj.ToObject<WebSocketMessage<StatusEvent>>();
                    return true;
                case WebSocketChannels.Ticker:
                case WebSocketChannels.TickerBatch:
                    message = obj.ToObject<WebSocketMessage<TickerEvent>>();
                    return true;
                case WebSocketChannels.Level2Data:
                    message = obj.ToObject<WebSocketMessage<Level2Event>>();
                    return true;
                case WebSocketChannels.User:
                    message = obj.ToObject<WebSocketMessage<UserEvent>>();
                    return true;
                case WebSocketChannels.Subscriptions:
                    message = obj.ToObject<WebSocketMessage<SubscriptionEvent>>();
                    return true;
                default:
                    message = json;
                    return false;
            }
        }

        #endregion // Event Handlers

        #region Dispose

        public void Dispose()
        {
            Disconnect();

            _socket?.Dispose();
        }

        #endregion // Dispose
    }
}
