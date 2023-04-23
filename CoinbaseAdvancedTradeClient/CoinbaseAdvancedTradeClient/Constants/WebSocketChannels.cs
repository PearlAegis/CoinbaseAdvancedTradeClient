namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class WebSocketChannels
    {
        public const string Status = "status";
        public const string Ticker = "ticker";
        public const string TickerBatch = "ticker_batch";
        public const string Level2 = "level2";
        public const string User = "user";
        public const string MarketTrades = "market_trades";

        //Not actual channels, but used in parsing messages.
        public const string Channel = "channel";
        public const string Level2Data = "l2_data";

        public readonly static ICollection<string> WebSocketChannelList = new List<string>
        {
            Status, 
            Ticker, 
            TickerBatch, 
            Level2, 
            User, 
            MarketTrades
        };
    }
}
