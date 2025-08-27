using CoinbaseAdvancedTradeClient.Resources;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using System.Globalization;
using System.Text;

namespace CoinbaseAdvancedTradeClient.Authentication
{
    public static class SecretApiKeyAuthenticator
    {
        public static string GenerateBearerJWT(string keyName, string keySecret, string requestMethod, string requestHost, string requestPath)
        {
            if (string.IsNullOrWhiteSpace(keyName)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(keyName));
            if (string.IsNullOrWhiteSpace(keySecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(keySecret));
            if (string.IsNullOrWhiteSpace(requestMethod)) throw new ArgumentNullException(nameof(requestMethod), ErrorMessages.RequestMethodRequired);
            if (string.IsNullOrWhiteSpace(requestHost)) throw new ArgumentNullException(nameof(requestHost), ErrorMessages.RequestHostRequired);
            if (string.IsNullOrWhiteSpace(requestPath)) throw new ArgumentNullException(nameof(requestPath), ErrorMessages.RequestPathRequired);

            // Decode the Ed25519 private key from base64
            byte[] decoded;
            try
            {
                decoded = Convert.FromBase64String(keySecret);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(ErrorMessages.InvalidBase64KeyFormat, nameof(keySecret), ex);
            }

            // Ed25519 keys are 64 bytes (32 bytes seed + 32 bytes public key)
            if (decoded.Length != 64)
            {
                throw new ArgumentException(ErrorMessages.InvalidEd25519KeyLength, nameof(keySecret));
            }

            // Extract the seed (first 32 bytes)
            byte[] seed = new byte[32];
            Array.Copy(decoded, 0, seed, 0, 32);

            // Create Ed25519 private key parameters
            var privateKey = new Ed25519PrivateKeyParameters(seed, 0);

            // Create the URI
            string uri = $"{requestMethod.ToUpperInvariant()} {requestHost}{requestPath}";

            // Create header
            var header = new Dictionary<string, object>
            {
                { "alg", "EdDSA" },
                { "typ", "JWT" },
                { "kid", keyName },
                { "nonce", GenerateNonce() }
            };

            // Create payload with timing
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var payload = new Dictionary<string, object>
            {
                { "sub", keyName },
                { "iss", "cdp" },
                { "aud", new[] { "cdp_service" } },
                { "nbf", now },
                { "exp", now + 120 }, // 2 minutes expiration
                { "uri", uri }
            };

            // Encode header and payload
            string headerJson = JsonConvert.SerializeObject(header);
            string payloadJson = JsonConvert.SerializeObject(payload);

            string encodedHeader = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
            string encodedPayload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

            string message = $"{encodedHeader}.{encodedPayload}";

            // Sign with Ed25519
            var signer = new Ed25519Signer();
            signer.Init(true, privateKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            signer.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] signature = signer.GenerateSignature();

            string encodedSignature = Base64UrlEncode(signature);

            return $"{message}.{encodedSignature}";
        }

        private static string GenerateNonce()
        {
            var random = new Random();
            var nonce = new char[16];
            for (int i = 0; i < 16; i++)
            {
                nonce[i] = (char)('0' + random.Next(10));
            }
            return new string(nonce);
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}