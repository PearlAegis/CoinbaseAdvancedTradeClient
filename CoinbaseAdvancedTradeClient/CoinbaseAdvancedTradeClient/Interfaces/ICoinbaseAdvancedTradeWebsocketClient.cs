using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public bool IsConnected { get; }
        Task<bool> ConnectAsync(Action<object?, bool> messageReceivedCallback, Action<object?, EventArgs>? openedCallback = null, Action<object?, EventArgs>? closedCallback = null, Action<Exception>? errorCallback = null);
        public void Subscribe(string channel, List<string> productIds);
        public void Unsubscribe(string channel, List<string> productIds);
        public void Disconnect();
    }
}
