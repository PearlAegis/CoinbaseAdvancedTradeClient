using CoinbaseAdvancedTradeClient.Models.Config;
using Microsoft.Extensions.Options;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests
{
    public class CoinbaseAdvancedTradeApiClientTests
    {
        [Fact]
        public void Constructor_NullConfig_ThrowsArgumentNullException()
        {
            //Arrange
            IOptions<CoinbaseClientConfig> config = null;

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
            var configValue = new CoinbaseClientConfig()
            {
                KeyName = key,
                KeySecret = secret
            };
            var config = Options.Create(configValue);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => 
            {
                var result = new CoinbaseAdvancedTradeApiClient(config); 
            });
        }
    }
}
