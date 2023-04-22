using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public WebSocket Socket { get; }
        public bool IsConnected { get; }
        public bool IsSubscribed { get; }
        public Task<bool> ConnectAsync(List<string> productIds, string channel, Action<object> messageReceived);
        public void Subscribe();
        public void Unsubscribe();
        public void Disconnect();
    }
}
