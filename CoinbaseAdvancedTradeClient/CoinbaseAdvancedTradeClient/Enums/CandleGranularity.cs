using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Enums
{
    public enum CandleGranularity
    {
        [EnumMember(Value = "ONE_MINUTE")]
        OneMinute,

        [EnumMember(Value = "FIVE_MINUTE")]
        FiveMinute,
        
        [EnumMember(Value = "FIFTEEN_MINUTE")]
        FifteenMinute,
        
        [EnumMember(Value = "THIRTY_MINUTE")]
        ThirtyMinute,

        [EnumMember(Value = "ONE_HOUR")]
        OneHour,

        [EnumMember(Value = "TWO_HOUR")]
        TwoHour,

        [EnumMember(Value = "SIX_HOUR")]
        SixHour,

        [EnumMember(Value = "ONE_DAY")]
        OneDay
    }
}
