using CoinbaseAdvancedTradeClient.Models.Config;

namespace CoinbaseAdvancedTradeClient.UnitTests.TestHelpers
{
    public static class TestConfigHelper
    {
        // Generate a valid Ed25519 key for testing purposes
        public static string GenerateTestKeySecret()
        {
            var keyBytes = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                keyBytes[i] = (byte)(i % 256); // Fill with test pattern
            }
            return Convert.ToBase64String(keyBytes);
        }

        public static SecretApiKeyConfig CreateTestApiConfig()
        {
            return new SecretApiKeyConfig()
            {
                KeyName = "test-key",
                KeySecret = GenerateTestKeySecret()
            };
        }

        public static SecretApiKeyWebSocketConfig CreateTestWebSocketConfig()
        {
            return new SecretApiKeyWebSocketConfig()
            {
                KeyName = "test-key",
                KeySecret = GenerateTestKeySecret()
            };
        }
    }
}