using CoinbaseAdvancedTradeClient.Models.WebSocket;
using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public WebSocket Socket { get; }
        public Task<bool> ConnectAsync(List<string> productIds, string channel);
        public void Subscribe();
        public void Unsubscribe();
        public void Disconnect();
    }
}
