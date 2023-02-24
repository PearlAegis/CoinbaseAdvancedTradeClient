using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class FeeTier
    {
        [JsonProperty("pricing_tier")]
        public string PricingTier { get; set; }

        [JsonProperty("usd_from")]
        public string UsdFrom { get; set; }

        [JsonProperty("usd_to")]
        public string UsdTo { get; set; }

        [JsonProperty("taker_fee_rate")]
        public string TakerFeeRate { get; set; }

        [JsonProperty("maker_fee_rate")]
        public string MakerFeeRate { get; set; }
    }
}
