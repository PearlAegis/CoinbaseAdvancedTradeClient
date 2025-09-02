using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using System.Net.Sockets;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests
{
    public class CoinbaseAdvancedTradeWebSocketClientTests
    {
        [Fact]
        public void Constructor_NullConfig_ThrowsArgumentNullException()
        {
            //Arrange
            CoinbaseClientConfig config = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = new CoinbaseAdvancedTradeWebSocketClient(config);
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
            CoinbaseClientConfig config = new CoinbaseClientConfig()
            {
                KeyName = key,
                KeySecret = secret
            };

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var result = new CoinbaseAdvancedTradeWebSocketClient(config);
            });
        }

        #region Connection

        [Fact]
        public async Task ConnectAsync_NullMessageReceivedCallback_ThrowsArgumentNullException()
        {
            //Arrange
            var testClient = CreateTestClient();

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                var result = await testClient.ConnectAsync(null);
            });
        }

        #endregion // Connection

        #region Subscription

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("TEST")]
        public void Subscribe_InvalidChannel_ThrowsArgumentException(string channel)
        {
            //Arrange
            var productIds = new List<string> { "BTC-USD" };

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                testClient.Subscribe(channel, productIds);
            });
        }

        [Fact]
        public void Subscribe_NullProductIds_ThrowsArgumentNullException()
        {
            //Arrange
            var channel = WebSocketChannels.Ticker;

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                testClient.Subscribe(channel, null);
            });
        }

        [Fact]
        public void Subscribe_EmptyProductIds_ThrowsArgumentNullException()
        {
            //Arrange
            var channel = WebSocketChannels.Ticker;
            var productIds = new List<string>();

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                testClient.Subscribe(channel, productIds);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("TEST")]
        public void Unsubscribe_InvalidChannel_ThrowsArgumentException(string channel)
        {
            //Arrange
            var productIds = new List<string> { "BTC-USD" };

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                testClient.Unsubscribe(channel, productIds);
            });
        }

        [Fact]
        public void Unsubscribe_NullProductIds_ThrowsArgumentNullException()
        {
            //Arrange
            var channel = WebSocketChannels.Ticker;

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                testClient.Unsubscribe(channel, null);
            });
        }

        [Fact]
        public void Unsubscribe_EmptyProductIds_ThrowsArgumentNullException()
        {
            //Arrange
            var channel = WebSocketChannels.Ticker;
            var productIds = new List<string>();

            var testClient = CreateTestClient();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                testClient.Unsubscribe(channel, productIds);
            });
        }

        #endregion // Subscription

        private ICoinbaseAdvancedTradeWebSocketClient CreateTestClient()
        {
            CoinbaseClientConfig config = new CoinbaseClientConfig()
            {
                KeyName = "testKey",
                KeySecret = TestHelpers.TestConfigHelper.GenerateTestKeySecret()
            };

            return new CoinbaseAdvancedTradeWebSocketClient(config);
        }
    }
}
