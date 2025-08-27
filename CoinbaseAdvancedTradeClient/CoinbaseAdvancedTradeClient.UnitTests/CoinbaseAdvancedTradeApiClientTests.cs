using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.UnitTests.TestHelpers;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests
{
    public class CoinbaseAdvancedTradeApiClientTests
    {
        [Fact]
        public void Constructor_NullConfig_ThrowsArgumentNullException()
        {
            //Arrange
            SecretApiKeyConfig config = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                var result = new CoinbaseAdvancedTradeApiClient(config); 
            });
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Test", "")]
        [InlineData("", "Test")]
        [InlineData(" ", "Test")]
        [InlineData("Test", "  ")]
        public void Constructor_EmptyConfigSetting_ThrowsArgumentException(string key, string secret)
        {
            //Arrange
            SecretApiKeyConfig config = new SecretApiKeyConfig()
            {
                KeyName = key,
                KeySecret = string.IsNullOrWhiteSpace(secret) ? secret : TestConfigHelper.GenerateTestKeySecret()
            };

            //Act & Assert
            Assert.Throws<ArgumentException>(() => 
            {
                var result = new CoinbaseAdvancedTradeApiClient(config); 
            });
        }
    }
}
