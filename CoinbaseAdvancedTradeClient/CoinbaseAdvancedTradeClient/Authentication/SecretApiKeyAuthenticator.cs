using CoinbaseAdvancedTradeClient.Resources;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
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

            // Parse the EC private key from PEM format
            ECPrivateKeyParameters privateKey;
            try
            {
                using var stringReader = new StringReader(keySecret);
                var pemReader = new PemReader(stringReader);
                var keyObject = pemReader.ReadObject();
                
                if (keyObject is not ECPrivateKeyParameters ecKey)
                {
                    throw new ArgumentException(ErrorMessages.InvalidECKeyFormat, nameof(keySecret));
                }
                
                privateKey = ecKey;
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                throw new ArgumentException(ErrorMessages.InvalidECKeyFormat, nameof(keySecret), ex);
            }

            // Create the URI
            string uri = $"{requestMethod.ToUpperInvariant()} {requestHost}{requestPath}";

            // Create header
            var header = new Dictionary<string, object>
            {
                { "alg", "ES256" },
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

            // Sign with ECDSA (ES256)
            var signer = new ECDsaSigner();
            signer.Init(true, privateKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hash = DigestUtilities.CalculateDigest("SHA-256", messageBytes);
            var signature = signer.GenerateSignature(hash);
            
            // Convert DER signature to IEEE P1363 format (r|s)
            var r = signature[0].ToByteArrayUnsigned();
            var s = signature[1].ToByteArrayUnsigned();
            
            // Ensure both r and s are 32 bytes (pad with leading zeros if needed)
            if (r.Length < 32)
            {
                var padded = new byte[32];
                Array.Copy(r, 0, padded, 32 - r.Length, r.Length);
                r = padded;
            }
            if (s.Length < 32)
            {
                var padded = new byte[32];
                Array.Copy(s, 0, padded, 32 - s.Length, s.Length);
                s = padded;
            }
            
            // Combine r and s
            var signatureBytes = new byte[64];
            Array.Copy(r, 0, signatureBytes, 0, 32);
            Array.Copy(s, 0, signatureBytes, 32, 32);

            string encodedSignature = Base64UrlEncode(signatureBytes);

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