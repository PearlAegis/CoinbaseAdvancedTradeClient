using CoinbaseAdvancedTradeClient.Models.Config;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests
{
    public class CoinbaseAdvancedTradeApiClientTests
    {
        [Fact]
        public void ValidateConfig_ValidConfig_ConfigPropertyIsSet()
        {
            //Arrange
            var key = "key";
            var secret = "secret";

            var config = new ApiClientConfig()
            {
                ApiKey = key,
                ApiSecret = secret
            };

            //Act
            var client = new CoinbaseAdvancedTradeApiClient(config);

            //Assert
            Assert.NotNull(client.Config);
            Assert.Equal(key, client.Config.ApiKey);
            Assert.Equal(secret, client.Config.ApiSecret);
        }

        [Fact]
        public void ValidateConfig_NullConfig_ThrowsArgumentNullException()
        {
            //Arrange
            ApiClientConfig config = null;

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
        public void ValidateConfig_EmptyConfigSetting_ThrowsArgumentException(string key, string secret)
        {
            //Arrange
            ApiClientConfig config = new ApiClientConfig()
            {
                ApiKey = key,
                ApiSecret = secret
            };

            //Act & Assert
            Assert.Throws<ArgumentException>(() => 
            {
                var result = new CoinbaseAdvancedTradeApiClient(config); 
            });
        }
    }
}
