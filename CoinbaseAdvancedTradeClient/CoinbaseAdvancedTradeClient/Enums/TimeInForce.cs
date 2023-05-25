using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Enums
{
    public enum TimeInForce
    {
        [EnumMember(Value = "GOOD_UNTIL_CANCELLED")]
        GoodUntilCancelled,

        [EnumMember(Value = "GOOD_UNTIL_DATE_TIME")]
        GoodUntilDate,

        [EnumMember(Value = "IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel
    }
}
