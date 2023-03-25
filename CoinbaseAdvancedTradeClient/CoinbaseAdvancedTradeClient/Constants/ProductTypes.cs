namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class ProductTypes
    {
        public const string Spot = "spot";

        public readonly static ICollection<string> ProductTypeList = new List<string>
        {
            Spot
        };
    }
}
