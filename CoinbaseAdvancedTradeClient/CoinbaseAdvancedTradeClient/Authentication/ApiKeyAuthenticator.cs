using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CoinbaseAdvancedTradeClient.Authentication
{
    public static class ApiKeyAuthenticator
    {
        public static string GenerateSignature(string apiSecret, string timestamp, string method, string requestPath, string body)
        {
            return Sign(apiSecret, timestamp + method + requestPath + body);
        }

        internal static string Sign(string apiSecret, string data)
        {
            var hmacKey = Convert.FromBase64String(apiSecret);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(hmacKey))
            {
                var signature = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(signature);
            }
        }

        public static string GenerateTimestamp()
        {
            var totalSeconds = (long)(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;
            var timestamp = totalSeconds.ToString("D", CultureInfo.InvariantCulture);

            return timestamp;
        }
    }
}
