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
