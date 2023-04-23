using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public bool IsConnected { get; }
        public bool IsSubscribed { get; }
        public Task<bool> ConnectAsync(Action<object> messageReceived);
        public void Subscribe(List<string> productIds, string channel);
        public void Unsubscribe();
        public void Disconnect();
    }
}
