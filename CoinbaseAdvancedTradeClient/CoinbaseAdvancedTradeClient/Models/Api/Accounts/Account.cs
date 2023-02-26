using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Accounts
{
    public class Account
    {
        [JsonProperty("uuid")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("available_balance")]
        public CurrencyValue AvailableBalance { get; set; } = new CurrencyValue();

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("ready")]
        public bool Ready { get; set; }

        [JsonProperty("hold")]
        public CurrencyValue Hold { get; set; } = new CurrencyValue();
    }
}
