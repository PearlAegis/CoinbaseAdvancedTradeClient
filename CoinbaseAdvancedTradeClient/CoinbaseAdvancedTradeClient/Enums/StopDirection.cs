using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StopDirection
    {
        [EnumMember(Value = "STOP_DIRECTION_STOP_UP")]
        Up,

        [EnumMember(Value = "STOP_DIRECTION_STOP_DOWN")]
        Down
    }
}
