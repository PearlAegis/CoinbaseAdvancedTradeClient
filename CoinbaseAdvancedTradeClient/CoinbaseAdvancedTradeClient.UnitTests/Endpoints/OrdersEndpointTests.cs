using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.Pages;
using FakeItEasy;
using Flurl.Http;
using Flurl.Http.Testing;
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

                result = await _testClient.Orders.GetListOrdersAsync();
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

                result = await _testClient.Orders.GetListOrdersAsync();
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

                result = await _testClient.Orders.GetListOrdersAsync();
            }

            //Assert
            Assert.NotNull(result.Data.Orders);
            Assert.Contains(result.Data.Orders, o => o.Id.Equals("0000-000000-000000", StringComparison.InvariantCultureIgnoreCase));
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
        [InlineData(" ", "OrderType", "OrderPlacementSource", 1)]
        [InlineData("OrderType", "OrderType", "OrderPlacementSource", 1)]
        [InlineData("OrderType", " ", "OrderPlacementSource", 1)]
        [InlineData("OrderType", "OrderType", " ", 1)]
        [InlineData("OrderType", "OrderType", "OrderPlacementSource", -1)]
        public async Task GetListOrdersAsync_InvalidParameters_ReturnsUnsuccessfulApiResponse(string productType, string orderType, string orderPlacementSource, int limit)
        {
            //Arrange
            ApiResponse<OrdersPage> result;

            var json = GetOrdersListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListOrdersAsync(productType: productType, orderType: orderType, orderPlacementSource: orderPlacementSource, limit: limit);
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
            ApiResponse<FillsPage> result;

            var json = GetFillsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetListFillsAsync();
            }

            //Assert
            Assert.NotNull(result.Data.Fills);
        }

        [Fact]
        public async Task GetListFillsAsync_ValidRequestAndResponseJson_ResponseHasValidFills()
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
            Assert.NotNull(result.Data.Fills);
            Assert.Contains(result.Data.Fills, f => f.EntryId.Equals("22222-2222222-22222222", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task GetListFillsAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<FillsPage> result;

            var invalidJson = GetInvalidFillsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Orders.GetListFillsAsync();
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
        [InlineData(251)]
        [InlineData(-25)]
        public async Task GetListFillsAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange
            ApiResponse<FillsPage> result;

            var json = GetFillsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);
                result = await _testClient.Orders.GetListFillsAsync(limit: limit);
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
        public async Task GetListFillsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<FillsPage> result;

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Orders.GetListFillsAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetListFillsAsync

        #region GetOrderAsync

        [Fact]
        public async Task GetOrderAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Order> result;

            var orderId = "0000-000000-000000";

            var json = GetOrderJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetOrderAsync(orderId);
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
        public async Task GetOrderAsync_ValidRequestAndResponseJson_ResponseHasValidOrder()
        {
            //Arrange
            ApiResponse<Order> result;

            var orderId = "0000-000000-000000";

            var expectedDate = DateTime.Parse("2021-05-31T09:59:59Z", styles: System.Globalization.DateTimeStyles.AdjustToUniversal);

            var json = GetOrderJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.GetOrderAsync(orderId);
            }

            //Assert
            Assert.NotNull(result.Data);
            Assert.Equal("0000-000000-000000", result.Data.Id);
            Assert.Equal("BTC-USD", result.Data.ProductId);
            Assert.Equal("2222-000000-000000", result.Data.UserId);
            Assert.Equal("10.00", result.Data.OrderConfiguration.MarketIoc.QuoteSize);
            Assert.Equal("0.001", result.Data.OrderConfiguration.MarketIoc.BaseSize);
            Assert.Equal("0.001", result.Data.OrderConfiguration.LimitGtc.BaseSize);
            Assert.Equal("10000.00", result.Data.OrderConfiguration.LimitGtc.LimitPrice);
            Assert.False(result.Data.OrderConfiguration.LimitGtc.PostOnly);
            Assert.Equal("0.001", result.Data.OrderConfiguration.LimitGtd.BaseSize);
            Assert.Equal("10000.00", result.Data.OrderConfiguration.LimitGtd.LimitPrice);
            Assert.Equal(expectedDate, result.Data.OrderConfiguration.LimitGtd.EndTime);
            Assert.False(result.Data.OrderConfiguration.LimitGtd.PostOnly);
            Assert.Equal("0.001", result.Data.OrderConfiguration.StopLimitGtc.BaseSize);
            Assert.Equal("10000.00", result.Data.OrderConfiguration.StopLimitGtc.LimitPrice);
            Assert.Equal("20000.00", result.Data.OrderConfiguration.StopLimitGtc.StopPrice);
            Assert.Equal(StopDirection.Up, result.Data.OrderConfiguration.StopLimitGtc.StopDirection);
            Assert.Equal("0.001", result.Data.OrderConfiguration.StopLimitGtd.BaseSize);
            Assert.Equal("10000.00", result.Data.OrderConfiguration.StopLimitGtd.LimitPrice);
            Assert.Equal("20000.00", result.Data.OrderConfiguration.StopLimitGtd.StopPrice);
            Assert.Equal(expectedDate, result.Data.OrderConfiguration.StopLimitGtd.EndTime);
            Assert.Equal(StopDirection.Up, result.Data.OrderConfiguration.StopLimitGtd.StopDirection);
            Assert.Equal("BUY", result.Data.Side);
            Assert.Equal("11111-000000-000000", result.Data.ClientOrderId);
            Assert.Equal("OPEN", result.Data.Status);
            Assert.Equal("UNKNOWN_TIME_IN_FORCE", result.Data.TimeInForce);
            Assert.Equal(expectedDate, result.Data.CreatedTime);
            Assert.Equal("50", result.Data.CompletionPercentage);
            Assert.Equal("0.001", result.Data.FilledSize);
            Assert.Equal("50", result.Data.AverageFilledPrice);
            Assert.Equal("1.23", result.Data.Fee);
            Assert.Equal("2", result.Data.NumberOfFills);
            Assert.Equal("10000", result.Data.FilledValue);
            Assert.True(result.Data.PendingCancel);
            Assert.False(result.Data.SizeInQuote);
            Assert.Equal("5.00", result.Data.TotalFees);
            Assert.False(result.Data.SizeInclusiveOfFees);
            Assert.Equal("123.45", result.Data.TotalValueAfterFees);
            Assert.Equal("UNKNOWN_TRIGGER_STATUS", result.Data.TriggerStatus);
            Assert.Equal("UNKNOWN_ORDER_TYPE", result.Data.OrderType);
            Assert.Equal("REJECT_REASON_UNSPECIFIED", result.Data.RejectReason);
            Assert.True(result.Data.Settled);
            Assert.Equal("SPOT", result.Data.ProductType);
            Assert.Equal("string", result.Data.RejectMessage);
            Assert.Equal("string", result.Data.CancelMessage);
            Assert.Equal("RETAIL_ADVANCED", result.Data.OrderPlacementSource);
        }

        [Fact]
        public async Task GetOrderAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Order> result;

            var orderId = "0000-000000-000000";

            var invalidJson = GetInvalidOrderJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Orders.GetOrderAsync(orderId);
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
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData(null)]
        public async Task GetOrderAsync_NullOrWhitespaceOrderId_ReturnsUnsuccessfulApiResponse(string orderId)
        {
            //Arrange
            ApiResponse<Order> result;

            var json = GetOrderJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);
                result = await _testClient.Orders.GetOrderAsync(orderId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetOrderAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Order> result;

            var orderId = "0000-000000-000000";

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Orders.GetOrderAsync(orderId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetOrderAsync

        #region PostCreateOrderAsync

        [Fact]
        public async Task PostCreateOrderAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
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
        public async Task PostCreateOrderAsync_ValidRequestAndResponseJson_ResponseHasValidCreateOrderSuccessResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
            }

            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.SuccessResponse);
            Assert.Null(result.Data.ErrorResponse);
            Assert.Equal("Test123", result.Data.OrderId, true);
            Assert.Equal("BTC-USD", result.Data.SuccessResponse.ProductId, true);
            Assert.Equal("BUY", result.Data.SuccessResponse.Side, true);
            Assert.Equal("Client123", result.Data.SuccessResponse.ClientOrderId, true);
        }

        [Fact]
        public async Task PostCreateOrderAsync_ValidRequestAndResponseJson_ResponseHasValidCreateOrderErrorResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            var json = GetValidCreateOrderErrorResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
            }

            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.False(result.Data.Success);
            Assert.NotNull(result.Data.ErrorResponse);
            Assert.Null(result.Data.SuccessResponse);
            Assert.Equal("Test123", result.Data.OrderId, true);
            Assert.Equal("Test failure reason.", result.Data.FailureReason, true);
            Assert.Equal("Test Error", result.Data.ErrorResponse.Error, true);
            Assert.Equal("Test error message.", result.Data.ErrorResponse.Message, true);
            Assert.Equal("Test error details.", result.Data.ErrorResponse.ErrorDetails, true);
            Assert.Equal("Preview failure reason", result.Data.ErrorResponse.PreviewFailureReason, true);
            Assert.Equal("New order failure reason", result.Data.ErrorResponse.NewOrderFailureReason, true);
        }

        [Fact]
        public async Task PostCreateOrderAsync_NullCreateOrderParameters_ThrowsArgumentNullException()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(null);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\t")]
        public async Task PostCreateOrderAsync_NullOrWhiteSpaceProductId_ThrowsArgumentException(string productId)
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = productId,
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
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
        public async Task PostCreateOrderAsync_NullOrderConfiguration_ThrowsArgumentException()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy
            };

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
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
        public async Task PostCreateOrderAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            var json = GetInvalidCreateOrderResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task PostCreateOrderAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var createOrder = new CreateOrderParameters
            {
                ProductId = "BTC-USD",
                Side = OrderSide.Buy,
                OrderConfiguration = A.Dummy<OrderConfiguration>()
            };

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Orders.PostCreateOrderAsync(createOrder);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // PostCreateOrderAsync

        #region PostCancelOrdersAsync

        [Fact]
        public async Task PostCancelOrders_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters
            {
                OrderIds = new List<string>() { "Test123", "Test456" }
            };

            var json = GetValidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
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
        public async Task PostCancelOrders_ValidRequestAndResponseJson_ResponseHasValidCancelOrdersResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters
            {
                OrderIds = new List<string>() { "Test123", "Test456" }
            };

            var json = GetValidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
            }

            //Assert
            Assert.NotNull(result.Data.Results);
            Assert.NotEmpty(result.Data.Results);
            Assert.Contains(result.Data.Results, x => x.Success == true && x.OrderId.Equals("Test123", StringComparison.InvariantCultureIgnoreCase) && x.FailureReason == null);
            Assert.Contains(result.Data.Results, x => x.Success == false && x.OrderId.Equals("Test456", StringComparison.InvariantCultureIgnoreCase) && x.FailureReason.Equals("Test failure reason.", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task PostCancelOrders_NullCancelOrdersParameters_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var json = GetValidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(null);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task PostCancelOrders_NullOrderIds_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters();

            var json = GetValidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task PostCancelOrders_EmptyOrderIds_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters
            {
                OrderIds = new List<string>()
            };

            var json = GetValidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }


        [Fact]
        public async Task PostCancelOrders_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters
            {
                OrderIds = new List<string>() { "Test123", "Test456" }
            };

            var json = GetInvalidCancelOrdersResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task PostCancelOrders_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CancelOrdersResponse> result;

            var cancelOrders = new CancelOrdersParameters
            {
                OrderIds = new List<string>() { "Test123", "Test456" }
            };

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Orders.PostCancelOrdersAsync(cancelOrders);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // PostCancelOrdersAsync

        #region CreateMarketOrderAsync

        [Fact]
        public async Task CreateMarketOrderAsync_ValidParameters_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.CreateMarketOrderAsync(orderSide, productId, amount);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\t")]
        public async Task CreateMarketOrderAsync_InvalidProductId_ThrowsArgumentNullException(string productId)
        {
            //Arrange
            var orderSide = OrderSide.Buy;
            var amount = 1.23m;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _testClient.Orders.CreateMarketOrderAsync(orderSide, productId, amount));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateMarketOrderAsync_InvalidAmount_ThrowsArgumentException(decimal amount)
        {
            //Arrange
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateMarketOrderAsync(orderSide, productId, amount));
        }

        #endregion // CreateMarketOrderAsync

        #region CreateLimitOrderAsync

        [Fact]
        public async Task CreateLimitOrderAsync_ValidParameters_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;
            var limitPrice = 123.45m;
            var postOnly = false;
            var endTime = DateTime.Now;

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.CreateLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, postOnly, endTime);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\t")]
        public async Task CreateLimitOrderAsync_InvalidProductId_ThrowsArgumentNullException(string productId)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var amount = 1.23m;
            var limitPrice = 123.45m;
            var postOnly = false;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _testClient.Orders.CreateLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, postOnly, endTime));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateLimitOrderAsync_InvalidAmount_ThrowsArgumentException(decimal amount)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var limitPrice = 123.45m;
            var postOnly = false;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, postOnly, endTime));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateLimitOrderAsync_InvalidLimitPrice_ThrowsArgumentException(decimal limitPrice)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;
            var postOnly = false;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, postOnly, endTime));
        }

        #endregion // CreateLimitOrderAsync

        #region CreateStopLimitOrderAsync

        [Fact]
        public async Task CreateStopLimitOrderAsync_ValidParameters_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CreateOrderResponse> result;

            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;
            var limitPrice = 123.45m;
            var stopPrice = 234.56m;
            var stopDirection = StopDirection.Up;
            var endTime = DateTime.Now;

            var json = GetValidCreateOrderSuccessResponse();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Orders.CreateStopLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, stopPrice, stopDirection, endTime);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\t")]
        public async Task CreateStopLimitOrderAsync_InvalidProductId_ThrowsArgumentNullException(string productId)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var amount = 1.23m;
            var limitPrice = 123.45m;
            var stopPrice = 234.56m;
            var stopDirection = StopDirection.Up;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _testClient.Orders.CreateStopLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, stopPrice, stopDirection, endTime));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateStopLimitOrderAsync_InvalidAmount_ThrowsArgumentException(decimal amount)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var limitPrice = 123.45m;
            var stopPrice = 234.56m;
            var stopDirection = StopDirection.Up;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateStopLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, stopPrice, stopDirection, endTime));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateStopLimitOrderAsync_InvalidLimitPrice_ThrowsArgumentException(decimal limitPrice)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;
            var stopPrice = 234.56m;
            var stopDirection = StopDirection.Up;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateStopLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, stopPrice, stopDirection, endTime));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.23)]
        public async Task CreateStopLimitOrderAsync_InvalidStopPrice_ThrowsArgumentException(decimal stopPrice)
        {
            //Arrange
            var timeInForce = TimeInForce.GoodTilDate;
            var orderSide = OrderSide.Buy;
            var productId = "TEST-USD";
            var amount = 1.23m;
            var limitPrice = 123.45m;
            var stopDirection = StopDirection.Up;
            var endTime = DateTime.Now;

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _testClient.Orders.CreateStopLimitOrderAsync(timeInForce, orderSide, productId, amount, limitPrice, stopPrice, stopDirection, endTime));
        }

        #endregion // CreateStopLimitOrderAsync

        #region Test Response Json

        private string GetOrdersListJsonString()
        {
            var json =
                """
                {
                    "orders": [
                        {
                            "order_id": "0000-000000-000000",
                            "product_id": "BTC-USD",
                            "user_id": "2222-000000-000000",
                            "order_configuration": {
                                "market_market_ioc": {
                                "quote_size": "10.00",
                                "base_size": "0.001"
                                },
                                "limit_limit_gtc": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "post_only": false
                                },
                                "limit_limit_gtd": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "end_time": "2021-05-31T09:59:59Z",
                                    "post_only": false
                                },
                                "stop_limit_stop_limit_gtc": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "stop_price": "20000.00",
                                    "stop_direction": "STOP_DIRECTION_STOP_UP"
                                },
                                "stop_limit_stop_limit_gtd": {
                                    "base_size": 0.001,
                                    "limit_price": "10000.00",
                                    "stop_price": "20000.00",
                                    "end_time": "2021-05-31T09:59:59Z",
                                    "stop_direction": "STOP_DIRECTION_STOP_UP"
                                }
                            },
                            "side": "BUY",
                            "client_order_id": "11111-000000-000000",
                            "status": "OPEN",
                            "time_in_force": "UNKNOWN_TIME_IN_FORCE",
                            "created_time": "2021-05-31T09:59:59Z",
                            "completion_percentage": "50",
                            "filled_size": "0.001",
                            "average_filled_price": "50",
                            "fee": "123.45",
                            "number_of_fills": "2",
                            "filled_value": "10000",
                            "pending_cancel": true,
                            "size_in_quote": false,
                            "total_fees": "5.00",
                            "size_inclusive_of_fees": false,
                            "total_value_after_fees": "string",
                            "trigger_status": "UNKNOWN_TRIGGER_STATUS",
                            "order_type": "UNKNOWN_ORDER_TYPE",
                            "reject_reason": "REJECT_REASON_UNSPECIFIED",
                            "settled": true,
                            "product_type": "SPOT",
                            "reject_message": "string",
                            "cancel_message": "string",
                            "order_placement_source": "RETAIL_ADVANCED"
                        }
                    ],
                    "sequence": "12345",
                    "has_next": true,
                    "cursor": "789100"
                }
                """;

            return json;
        }

        private string GetInvalidOrdersListJsonString()
        {
            var json =
                """
                {
                    "orders": [
                        {
                            "order_id": "0000-000000-000000",
                            "product_id": "BTC-USD",
                            "user_id": "2222-000000-000000",
                            "order_configuration": {
                                "market_market_ioc": {
                                "quote_size": "10.00",
                                "base_size": "0.001"
                                },
                                "limit_limit_gtc": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "post_only": "INVALID"
                                },
                                "limit_limit_gtd": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "end_time": "2021-05-31T09:59:59Z",
                                    "post_only": false
                                },
                                "stop_limit_stop_limit_gtc": {
                                    "base_size": "0.001",
                                    "limit_price": "10000.00",
                                    "stop_price": "20000.00",
                                    "stop_direction": "STOP_DIRECTION_STOP_UP"
                                },
                                "stop_limit_stop_limit_gtd": {
                                    "base_size": 0.001,
                                    "limit_price": "10000.00",
                                    "stop_price": "20000.00",
                                    "end_time": "2021-05-31T09:59:59Z",
                                    "stop_direction": "STOP_DIRECTION_STOP_UP"
                                }
                            },
                            "side": "BUY",
                            "client_order_id": "11111-000000-000000",
                            "status": "OPEN",
                            "time_in_force": "UNKNOWN_TIME_IN_FORCE",
                            "created_time": "2021-05-31T09:59:59Z",
                            "completion_percentage": "50",
                            "filled_size": "0.001",
                            "average_filled_price": "50",
                            "fee": "123.45",
                            "number_of_fills": "2",
                            "filled_value": "10000",
                            "pending_cancel": true,
                            "size_in_quote": false,
                            "total_fees": "5.00",
                            "size_inclusive_of_fees": false,
                            "total_value_after_fees": "string",
                            "trigger_status": "UNKNOWN_TRIGGER_STATUS",
                            "order_type": "UNKNOWN_ORDER_TYPE",
                            "reject_reason": "REJECT_REASON_UNSPECIFIED",
                            "settled": true,
                            "product_type": "SPOT",
                            "reject_message": "string",
                            "cancel_message": "string",
                            "order_placement_source": "RETAIL_ADVANCED"
                        }
                    ],
                    "sequence": "12345",
                    "has_next": true,
                    "cursor": "789100"
                }
                """;

            return json;
        }

        private string GetFillsListJsonString()
        {
            var json =
                """
                {
                    "fills": [
                        {
                            "entry_id": "22222-2222222-22222222",
                            "trade_id": "1111-11111-111111",
                            "order_id": "0000-000000-000000",
                            "trade_time": "2021-05-31T09:59:59Z",
                            "trade_type": "FILL",
                            "price": "10000.00",
                            "size": "0.001",
                            "commission": "1.25",
                            "product_id": "BTC-USD",
                            "sequence_timestamp": "2021-05-31T09:58:59Z",
                            "liquidity_indicator": "UNKNOWN_LIQUIDITY_INDICATOR",
                            "size_in_quote": false,
                            "user_id": "3333-333333-3333333",
                            "side": "BUY"
                        }
                    ],
                    "cursor": "789100"
                }
                """;

            return json;
        }

        private string GetInvalidFillsListJsonString()
        {
            var json =
                """
                {
                    "fills": [
                        {
                            "entry_id": "22222-2222222-22222222",
                            "trade_id": "1111-11111-111111",
                            "order_id": "0000-000000-000000",
                            "trade_time": "2021-05-31T09:59:59Z",
                            "trade_type": "FILL",
                            "price": "10000.00",
                            "size": "0.001",
                            "commission": "1.25",
                            "product_id": "BTC-USD",
                            "sequence_timestamp": "2021-05-31T09:58:59Z",
                            "liquidity_indicator": "UNKNOWN_LIQUIDITY_INDICATOR",
                            "size_in_quote": "INVALID",
                            "user_id": "3333-333333-3333333",
                            "side": "BUY"
                        }
                    ],
                    "cursor": "789100"
                }
                """;

            return json;
        }

        private string GetOrderJsonString()
        {
            var json =
                """
                {
                    "order": {
                        "order_id": "0000-000000-000000",
                        "product_id": "BTC-USD",
                        "user_id": "2222-000000-000000",
                        "order_configuration": {
                            "market_market_ioc": {
                                "quote_size": "10.00",
                                "base_size": "0.001"
                            },
                            "limit_limit_gtc": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "post_only": false
                            },
                            "limit_limit_gtd": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "end_time": "2021-05-31T09:59:59Z",
                                "post_only": false
                            },
                            "stop_limit_stop_limit_gtc": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "stop_price": "20000.00",
                                "stop_direction": "STOP_DIRECTION_STOP_UP"
                            },
                            "stop_limit_stop_limit_gtd": {
                                "base_size": 0.001,
                                "limit_price": "10000.00",
                                "stop_price": "20000.00",
                                "end_time": "2021-05-31T09:59:59Z",
                                "stop_direction": "STOP_DIRECTION_STOP_UP"
                            }
                        },
                        "side": "BUY",
                        "client_order_id": "11111-000000-000000",
                        "status": "OPEN",
                        "time_in_force": "UNKNOWN_TIME_IN_FORCE",
                        "created_time": "2021-05-31T09:59:59Z",
                        "completion_percentage": "50",
                        "filled_size": "0.001",
                        "average_filled_price": "50",
                        "fee": "1.23",
                        "number_of_fills": "2",
                        "filled_value": "10000",
                        "pending_cancel": true,
                        "size_in_quote": false,
                        "total_fees": "5.00",
                        "size_inclusive_of_fees": false,
                        "total_value_after_fees": "123.45",
                        "trigger_status": "UNKNOWN_TRIGGER_STATUS",
                        "order_type": "UNKNOWN_ORDER_TYPE",
                        "reject_reason": "REJECT_REASON_UNSPECIFIED",
                        "settled": true,
                        "product_type": "SPOT",
                        "reject_message": "string",
                        "cancel_message": "string",
                        "order_placement_source": "RETAIL_ADVANCED"
                    }
                }
                """;

            return json;
        }

        private string GetInvalidOrderJsonString()
        {
            var json =
                """
                {
                    "order": {
                        "order_id": "0000-000000-000000",
                        "product_id": "BTC-USD",
                        "user_id": "2222-000000-000000",
                        "order_configuration": {
                            "market_market_ioc": {
                                "quote_size": "10.00",
                                "base_size": "0.001"
                            },
                            "limit_limit_gtc": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "post_only": "INVALID"
                            },
                            "limit_limit_gtd": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "end_time": "2021-05-31T09:59:59Z",
                                "post_only": false
                            },
                            "stop_limit_stop_limit_gtc": {
                                "base_size": "0.001",
                                "limit_price": "10000.00",
                                "stop_price": "20000.00",
                                "stop_direction": "STOP_DIRECTION_STOP_UP"
                            },
                            "stop_limit_stop_limit_gtd": {
                                "base_size": 0.001,
                                "limit_price": "10000.00",
                                "stop_price": "20000.00",
                                "end_time": "2021-05-31T09:59:59Z",
                                "stop_direction": "STOP_DIRECTION_STOP_UP"
                            }
                        },
                        "side": "BUY",
                        "client_order_id": "11111-000000-000000",
                        "status": "OPEN",
                        "time_in_force": "UNKNOWN_TIME_IN_FORCE",
                        "created_time": "2021-05-31T09:59:59Z",
                        "completion_percentage": "50",
                        "filled_size": "0.001",
                        "average_filled_price": "50",
                        "fee": "string",
                        "number_of_fills": "2",
                        "filled_value": "10000",
                        "pending_cancel": true,
                        "size_in_quote": false,
                        "total_fees": "5.00",
                        "size_inclusive_of_fees": false,
                        "total_value_after_fees": "string",
                        "trigger_status": "UNKNOWN_TRIGGER_STATUS",
                        "order_type": "UNKNOWN_ORDER_TYPE",
                        "reject_reason": "REJECT_REASON_UNSPECIFIED",
                        "settled": "boolean",
                        "product_type": "SPOT",
                        "reject_message": "string",
                        "cancel_message": "string",
                        "order_placement_source": "RETAIL_ADVANCED"
                    }
                }
                """;

            return json;
        }

        public string GetValidCreateOrderSuccessResponse()
        {
            var json =
                """
                {
                    "success": true,
                    "order_id": "Test123",
                    "success_response":
                    {
                        "order_id": "Test123",
                        "product_id": "BTC-USD",
                        "side": "BUY",
                        "client_order_id": "Client123"
                    }

                }
                """;

            return json;
        }

        public string GetValidCreateOrderErrorResponse()
        {
            var json =
                """
                {
                    "success": false,
                    "order_id": "Test123",
                    "failure_reason": "Test failure reason.",
                    "error_response":
                    {
                        "error": "Test Error",
                        "message": "Test error message.",
                        "error_details": "Test error details.",
                        "preview_failure_reason": "Preview failure reason",
                        "new_order_failure_reason": "New order failure reason"
                    }
                }
                """;

            return json;
        }

        public string GetInvalidCreateOrderResponse()
        {
            var json =
                """
                {
                    "success": "INVALID",
                    "order_id": "Test123",
                    "success_response":
                    {
                        "order_id": "Test123",
                        "product_id": "BTC-USD",
                        "side": "BUY",
                        "client_order_id": "Client123"
                    }

                }
                """;

            return json;
        }

        public string GetValidCancelOrdersResponse()
        {
            var json =
                """
                {
                    "results": [
                        {
                            "success": true,
                            "order_id": "Test123"
                        },
                        {
                            "success": false,
                            "order_id": "Test456",
                            "failure_reason": "Test failure reason.",
                        }
                    ]
                }
                """;

            return json;
        }

        public string GetInvalidCancelOrdersResponse()
        {
            var json =
                """
                {
                    "results": "INVALID"
                }
                """;

            return json;
        }

        #endregion // Test Response Json
    }
}
