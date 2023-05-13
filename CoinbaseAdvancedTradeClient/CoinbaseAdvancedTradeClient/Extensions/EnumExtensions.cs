using System.Runtime.Serialization;

namespace CoinbaseAdvancedTradeClient.Extensions
{
    internal static class EnumExtensions
    {
        internal static string? GetEnumMemberValue(this Enum member)
        {
            if (member != null)
            {
                var type = member.GetType();
                var info = type.GetMember(member.ToString());
                var attributes = info[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attributes.Length > 0)
                {
                    var enumMember = (EnumMemberAttribute)attributes[0];
                    return enumMember.Value;
                }
            }

            return null;
        }
    }
}
