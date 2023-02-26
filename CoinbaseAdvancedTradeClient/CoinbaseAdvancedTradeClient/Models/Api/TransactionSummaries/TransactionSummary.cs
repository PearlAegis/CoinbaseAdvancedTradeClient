using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class TransactionSummary
    {
        [JsonProperty("total_volume")]
        public double TotalVolume { get; set; }

        [JsonProperty("total_fees")]
        public double TotalFees { get; set; }

        [JsonProperty("fee_tier")]
        public FeeTier FeeTier { get; set; } = new FeeTier();

        [JsonProperty("margin_rate")]
        public MarginRate MarginRate { get; set; } = new MarginRate();

        [JsonProperty("goods_and_services_tax")]
        public GoodsAndServicesTax GoodsAndServicesTax { get; set; } = new GoodsAndServicesTax();

        [JsonProperty("advanced_trade_only_volume")]
        public double AdvancedTradeOnlyVolume { get; set; }

        [JsonProperty("advanced_trade_only_fees")]
        public double AdvancedTradeOnlyFees { get; set; }

        [JsonProperty("coinbase_pro_volume")]
        public double CoinbaseProVolume { get; set; }

        [JsonProperty("coinbase_pro_fees")]
        public double CoinbaseProFees { get; set; }
    }
}
