using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class TransactionSummary
    {
        [JsonProperty("total_volume")]
        public decimal? TotalVolume { get; set; }

        [JsonProperty("total_fees")]
        public decimal? TotalFees { get; set; }

        [JsonProperty("fee_tier")]
        public FeeTier FeeTier { get; set; }

        [JsonProperty("margin_rate")]
        public MarginRate MarginRate { get; set; }

        [JsonProperty("goods_and_services_tax")]
        public GoodsAndServicesTax GoodsAndServicesTax { get; set; }

        [JsonProperty("advanced_trade_only_volume")]
        public decimal? AdvancedTradeOnlyVolume { get; set; }

        [JsonProperty("advanced_trade_only_fees")]
        public decimal? AdvancedTradeOnlyFees { get; set; }

        [JsonProperty("coinbase_pro_volume")]
        public decimal? CoinbaseProVolume { get; set; }

        [JsonProperty("coinbase_pro_fees")]
        public decimal? CoinbaseProFees { get; set; }
    }
}
