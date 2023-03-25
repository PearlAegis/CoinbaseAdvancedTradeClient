namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class OrderTypes
    {
        public const string Market = "MARKET";
        public const string Limit = "LIMIT";
        public const string Stop = "STOP";
        public const string StopLimit = "STOP_LIMIT";

        public readonly static ICollection<string> OrderTypeList = new List<string>
        {
            Market,
            Limit,
            Stop,
            StopLimit
        };
    }
}
