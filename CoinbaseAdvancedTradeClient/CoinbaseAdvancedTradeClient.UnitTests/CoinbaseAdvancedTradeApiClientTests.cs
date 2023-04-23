using CoinbaseAdvancedTradeClient.Models.Config;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests
{
    public class CoinbaseAdvancedTradeApiClientTests
    {
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
