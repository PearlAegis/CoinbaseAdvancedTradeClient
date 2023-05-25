using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class FeeTier
    {
        [JsonProperty("pricing_tier")]
        public string PricingTier { get; set; }

        [JsonProperty("usd_from")]
        public decimal? UsdFrom { get; set; }

        [JsonProperty("usd_to")]
        public decimal? UsdTo { get; set; }

        [JsonProperty("taker_fee_rate")]
        public decimal? TakerFeeRate { get; set; }

        [JsonProperty("maker_fee_rate")]
        public decimal? MakerFeeRate { get; set; }
    }
}
