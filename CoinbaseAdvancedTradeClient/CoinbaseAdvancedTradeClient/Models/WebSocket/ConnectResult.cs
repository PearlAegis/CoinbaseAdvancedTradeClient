namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class ConnectResult
    {
        public bool Success { get; }
        public object Sender { get; }
        public EventArgs EventArgs { get; }

        public ConnectResult(bool success, object sender, EventArgs eventArgs)
        {
            Success = success;
            Sender = sender;
            EventArgs = eventArgs;
        }
    }
}
