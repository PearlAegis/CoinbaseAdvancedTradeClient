using CoinbaseAdvancedTradeClient.Models.WebSocket;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public Task<ConnectResult> ConnectAsync();
        public Task SubscribeAsync(List<string> productIds, string channel);
        public void Unsubscribe();
    }
}
