using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Product
    {
        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("base_increment")]
        public decimal? BaseIncrement { get; set; }

        [JsonProperty("quote_increment")]
        public decimal? QuoteIncrement { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("min_market_funds")]
        public decimal? MinMarketFunds { get; set; }
    }
}
