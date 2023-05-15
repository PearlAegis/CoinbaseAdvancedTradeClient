using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Enums
{
    public enum OrderType
    {
        [EnumMember(Value = "MARKET")]
        Market,

        [EnumMember(Value = "LIMIT")]
        Limit,

        [EnumMember(Value = "STOP")]
        Stop,

        [EnumMember(Value = "STOP_LIMIT")]
        StopLimit
    }
}
