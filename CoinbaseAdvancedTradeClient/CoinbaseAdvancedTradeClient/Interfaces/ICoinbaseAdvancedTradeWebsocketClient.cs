using CoinbaseAdvancedTradeClient.Models.WebSocket;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeWebSocketClient
    {
        public Task ConnectAsync();
        public Task SubscribeAsync(Subscription subscription);
    }
}
