using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.UnitTests.TestHelpers;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests.Authentication
{
    public class SecretApiKeyAuthenticatorTests
    {
        [Fact]
        public void GenerateBearerJWT_ValidParameters_ReturnsJWT()
        {
            // Arrange
            var testKeySecret = TestConfigHelper.GenerateTestKeySecret();
            
            // Act
            var result = SecretApiKeyAuthenticator.GenerateBearerJWT(
                "test-key-name",
                testKeySecret,
                "GET",
                "api.coinbase.com",
                "/v1/test"
            );

            // Assert
            Assert.NotNull(result);
            Assert.Contains(".", result);
            var parts = result.Split('.');
            Assert.Equal(3, parts.Length); // header.payload.signature
        }

        [Fact]
        public void GenerateBearerJWT_InvalidKeySecret_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                SecretApiKeyAuthenticator.GenerateBearerJWT(
                    "test-key-name",
                    "invalid-key-format",
                    "GET",
                    "api.coinbase.com",
                    "/v1/test"
                );
            });
        }
    }
}