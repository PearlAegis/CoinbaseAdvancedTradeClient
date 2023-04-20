using CoinbaseAdvancedTradeClient.Models.WebSocket;
using WebSocket4Net;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public WebSocket Socket { get; }
        public Task<bool> ConnectAsync();
        public Task SubscribeAsync(List<string> productIds, string channel);
        public void Unsubscribe();
    }
}
