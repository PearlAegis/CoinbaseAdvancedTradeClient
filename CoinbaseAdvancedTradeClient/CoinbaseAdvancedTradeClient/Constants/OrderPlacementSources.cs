namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class OrderPlacementSources
    {
        public const string RetailAdvanced = "RETAIL_ADVANCED";

        public readonly static ICollection<string> OrderPlacementSourceList = new List<string>
        {
            RetailAdvanced
        };
    }
}
