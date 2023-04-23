using CoinbaseAdvancedTradeClient.Resources;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CoinbaseAdvancedTradeClient.Authentication
{
    public static class ApiKeyAuthenticator
    {
        public static string GenerateTimestamp()
        {
            var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var timestamp = unixTime.ToString(CultureInfo.InvariantCulture);

            return timestamp;
        }

        public static string GenerateApiSignature(string apiSecret, string timestamp, string method, string requestPath, string body)
        {
            return Sign(apiSecret, timestamp + method + requestPath + body);
        }

        public static string GenerateWebSocketSignature(string apiSecret, string timestamp, string channel, ICollection<string> productIds)
        {
            if (string.IsNullOrWhiteSpace(channel)) throw new ArgumentNullException(nameof(channel), ErrorMessages.ChannelRequired);
            if (productIds == null || !productIds.Any()) throw new ArgumentNullException(nameof(productIds), ErrorMessages.ProductIdRequired);

            var products = string.Join(",", productIds);

            return Sign(apiSecret, timestamp + channel + products);
        }

        private static string Sign(string apiSecret, string data)
        {
            var apiSecretBytes = Encoding.UTF8.GetBytes(apiSecret);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(apiSecretBytes))
            {
                var hash = hmac.ComputeHash(dataBytes);
                var signature = Convert.ToHexString(hash).ToLowerInvariant();

                return signature;
            }
        }
    }
}
