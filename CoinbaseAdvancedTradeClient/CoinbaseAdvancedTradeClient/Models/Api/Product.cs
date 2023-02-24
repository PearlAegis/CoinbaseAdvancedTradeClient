using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class Product
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("price_percentage_change_24h")]
        public string PricePercentageChange24H { get; set; }

        [JsonProperty("volume_24h")]
        public string Volume24H { get; set; }

        [JsonProperty("volume_percentage_change_24h")]
        public string VolumePercentageChange24H { get; set; }

        [JsonProperty("base_increment")]
        public string BaseIncrement { get; set; }

        [JsonProperty("quote_increment")]
        public string QuoteIncrement { get; set; }

        [JsonProperty("quote_min_size")]
        public string QuoteMinSize { get; set; }

        [JsonProperty("quote_max_size")]
        public string QuoteMaxSize { get; set; }

        [JsonProperty("base_min_size")]
        public string BaseMinSize { get; set; }

        [JsonProperty("base_max_size")]
        public string BaseMaxSize { get; set; }

        [JsonProperty("base_name")]
        public string BaseName { get; set; }

        [JsonProperty("quote_name")]
        public string QuoteName { get; set; }

        [JsonProperty("watched")]
        public bool Watched  { get; set; }

        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }

        [JsonProperty("new")]
        public bool IsNew { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cancel_only")]
        public bool CancelOnly  { get; set; }

        [JsonProperty("limit_only")]
        public bool LimitOnly { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }

        [JsonProperty("trading_disabled")]
        public bool TradingDisabled  { get; set; }

        [JsonProperty("auction_mode")]
        public bool AuctionMode  { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("quote_currency_id")]
        public string QuoteCurrencyId { get; set; }

        [JsonProperty("base_currency_id")]
        public string BaseCurrencyId { get; set; }

        [JsonProperty("mid_market_price")]
        public string MidMarketPrice { get; set; }

        [JsonProperty("base_display_symbol")]
        public string BaseDisplaySymbol { get; set; }

        [JsonProperty("quote_display_symbol")]
        public string QuoteDisplaySymbol { get; set; }
    }
}
