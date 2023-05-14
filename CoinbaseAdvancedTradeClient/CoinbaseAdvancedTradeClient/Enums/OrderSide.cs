using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderSide
    {
        [EnumMember(Value = "BUY")]
        Buy,

        [EnumMember(Value = "SELL")]
        Sell
    }
}
