namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class OrderSides
    {
        public const string Buy = "BUY";
        public const string Sell = "SELL";

        public readonly static ICollection<string> OrderSideList = new List<string>
        {
            Buy,
            Sell
        };
    }
}
