using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl.Http;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests.Endpoints
{
    public class OrdersEndpointTests
    {
        private readonly ICoinbaseAdvancedTradeApiClient _testClient;

        public OrdersEndpointTests()
        {
            var config = new ApiClientConfig()
            {
                ApiKey = "key",
                ApiSecret = "secret"
            };

            _testClient = new CoinbaseAdvancedTradeApiClient(config);
        }

        #region GetListOrdersAsync

        [Fact]
        public async Task GetListOrdersAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListOrdersAsync(json);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetListOrdersAsync_ValidRequestAndResponseJson_ResultHasValidOrdersPage()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListOrdersAsync(json);
            }

            //Assert
            Assert.Null(result.Data.Order);
            Assert.NotNull(result.Data.Orders);
        }

        [Fact]
        public async Task GetListOrdersAsync_ValidRequestAndResponseJson_ResponseHasValidOrders()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListOrdersAsync(json);
            }

            //Assert
            Assert.NotNull(result.Data.Orders);
            Assert.Contains(result.Data.Orders, o => o.Id.Equals("BTC", StringComparison.InvariantCultureIgnoreCase));
            Assert.Contains(result.Data.Orders, o => o.Id.Equals("ETH", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task GetListOrdersAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var invalidJson = GetInvalidOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Orders.GetListOrdersAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(" ", "OrderSide", "OrderType", "OrderPlacementSource", 1)]
        [InlineData("OrderType", " ", "OrderType", "OrderPlacementSource", 1)]
        [InlineData("OrderType", "OrderSide", " ", "OrderPlacementSource", 1)]
        [InlineData("OrderType", "OrderSide", "OrderType", " ", 1)]
        [InlineData("OrderType", "OrderSide", "OrderType", "OrderPlacementSource", -1)]
        public async Task GetListOrdersAsync_InvalidParameters_ReturnsUnsuccessfulApiResponse(string productType, string orderSide, string orderType, string orderPlacementSource, int limit)
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var invalidJson = GetInvalidOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Orders.GetListOrdersAsync(productType: productType, orderSide: orderSide, orderType: orderType, orderPlacementSource: orderPlacementSource, limit: limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetListOrdersAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Orders.GetListOrdersAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetListOrdersAsync

        #region GetListFillsAsync

        [Fact]
        public async Task GetListFillsAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<FillsPage> result;

            var json = GetFillsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListFillsAsync(json);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetListFillsAsync_ValidRequestAndResponseJson_ResultHasValidFillsPage()
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListOrdersAsync(json);
            }

            //Assert
            Assert.Null(result.Data.Order);
            Assert.NotNull(result.Data.Orders);
        }

        [Fact]
        public async Task GetListFillsAsync_ValidRequestAndResponseJson_ResponseHasValidFills()
        {

        }

        [Fact]
        public async Task GetListFillsAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {

        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetListFillsAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {

        }

        [Fact]
        public async Task GetListFillsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {

        }

        #endregion // GetListFillsAsync

        #region GetOrderAsync

        [Fact]
        public async Task GetOrderAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {

        }

        [Fact]
        public async Task GetOrderAsync_ValidRequestAndResponseJson_ResultHasValidFillsPage()
        {

        }

        [Fact]
        public async Task GetOrderAsync_ValidRequestAndResponseJson_ResponseHasValidFills()
        {

        }

        [Fact]
        public async Task GetOrderAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {

        }

        [Fact]
        public async Task GetOrderAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse()
        {

        }

        [Fact]
        public async Task GetOrderAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {

        }

        #endregion // GetOrderAsync

        #region Test Response Json

        private string GetOrdersListJsonString()
        {
            var json =
                """

                """;

            return json;
        }

        private string GetInvalidOrdersListJsonString()
        {
            var json =
                """

                """;

            return json;
        }

        private string GetFillsListJsonString()
        {
            var json =
                """

                """;

            return json;
        }

        private string GetInvalidFillsListJsonString()
        {
            var json =
                """

                """;

            return json;
        }

        private string GetOrderJsonString()
        {
            var json =
                """

                """;

            return json;
        }

        private string GetInvalidOrderJsonString()
        {
            var json =
                """

                """;

            return json;
        }


        #endregion // Test Response Json
    }
}
