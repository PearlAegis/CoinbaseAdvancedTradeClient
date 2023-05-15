using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Products
{
    public class Product
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("price_percentage_change_24h")]
        public decimal? PricePercentageChange24H { get; set; }

        [JsonProperty("volume_24h")]
        public decimal? Volume24H { get; set; }

        [JsonProperty("volume_percentage_change_24h")]
        public decimal? VolumePercentageChange24H { get; set; }

        [JsonProperty("base_increment")]
        public decimal? BaseIncrement { get; set; }

        [JsonProperty("quote_increment")]
        public decimal? QuoteIncrement { get; set; }

        [JsonProperty("quote_min_size")]
        public decimal? QuoteMinSize { get; set; }

        [JsonProperty("quote_max_size")]
        public decimal? QuoteMaxSize { get; set; }

        [JsonProperty("base_min_size")]
        public decimal? BaseMinSize { get; set; }

        [JsonProperty("base_max_size")]
        public decimal? BaseMaxSize { get; set; }

        [JsonProperty("base_name")]
        public string BaseName { get; set; }

        [JsonProperty("quote_name")]
        public string QuoteName { get; set; }

        [JsonProperty("watched")]
        public bool Watched { get; set; }

        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }

        [JsonProperty("new")]
        public bool IsNew { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cancel_only")]
        public bool CancelOnly { get; set; }

        [JsonProperty("limit_only")]
        public bool LimitOnly { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }

        [JsonProperty("trading_disabled")]
        public bool TradingDisabled { get; set; }

        [JsonProperty("auction_mode")]
        public bool AuctionMode { get; set; }

        [JsonProperty("product_type")]
        public ProductType ProductType { get; set; }

        [JsonProperty("quote_currency_id")]
        public string QuoteCurrencyId { get; set; }

        [JsonProperty("base_currency_id")]
        public string BaseCurrencyId { get; set; }

        [JsonProperty("mid_market_price")]
        public decimal? MidMarketPrice { get; set; }

        [JsonProperty("base_display_symbol")]
        public string BaseDisplaySymbol { get; set; }

        [JsonProperty("quote_display_symbol")]
        public string QuoteDisplaySymbol { get; set; }
    }
}
