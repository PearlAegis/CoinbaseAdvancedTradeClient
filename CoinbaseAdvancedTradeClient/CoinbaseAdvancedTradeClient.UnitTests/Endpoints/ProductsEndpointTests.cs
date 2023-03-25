using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Products;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.Pages;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;
using Flurl.Http.Testing;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests.Endpoints
{
    public class ProductsEndpointTests
    {
        private readonly ICoinbaseAdvancedTradeApiClient _testClient;

        public ProductsEndpointTests()
        {
            var config = new ApiClientConfig()
            {
                ApiKey = "key",
                ApiSecret = "secret"
            };

            _testClient = new CoinbaseAdvancedTradeApiClient(config);
        }

        #region GetListProductsAsync

        [Fact]
        public async Task GetListProductsAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "SPOT";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
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
        public async Task GetListProductsAsync_ValidRequestAndResponseJson_ResponseHasValidProductsPage()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "SPOT";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result.Data.Products);
            Assert.Equal(100, result.Data.NumberOfProducts);
        }

        [Fact]
        public async Task GetListProductsAsync_ValidRequestAndResponseJson_ResponseHasValidProducts()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "SPOT";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result.Data.Products);
            Assert.True(result.Data.Products.Count > 0);
            Assert.Contains(result.Data.Products, p => p.ProductId.Equals("BTC-USD", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task GetListProductsAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "SPOT";

            var json = GetInvalidProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.Equal(nameof(FlurlParsingException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetListProductsAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var offset = 0;
            var productType = "SPOT";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.LimitParameterRange, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetListProductsAsync_InvalidOffsetRange_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = -1;
            var productType = "SPOT";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.OffsetParameterRange, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetListProductsAsync_InvalidProductType_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "TEST";

            var json = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.ProductTypeInvalid, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetListProductsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<ProductsPage> result;

            var limit = 1;
            var offset = 0;
            var productType = "SPOT";

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Products.GetListProductsAsync(limit, offset, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetListProductsAsync

        #region GetProductAsync

        [Fact]
        public async Task GetProductAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Product> result;

            var productId = "TEST";

            var json = GetProductJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductAsync(productId);
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
        public async Task GetProductAsync_ValidRequestAndResponseJson_ResponseHasValidProduct()
        {
            //Arrange
            ApiResponse<Product> result;

            var productId = "TEST";

            var json = GetProductJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductAsync(productId);
            }

            //Assert
            Assert.Equal("BTC-USD", result.Data.ProductId);
            Assert.Equal("140.21", result.Data.Price);
            Assert.Equal("9.43%", result.Data.PricePercentageChange24H);
            Assert.Equal("1908432", result.Data.Volume24H);
            Assert.Equal("9.43%", result.Data.VolumePercentageChange24H);
            Assert.Equal("0.00000001", result.Data.BaseIncrement);
            Assert.Equal("0.00000001", result.Data.QuoteIncrement);
            Assert.Equal("0.00000001", result.Data.QuoteMinSize);
            Assert.Equal("1000", result.Data.QuoteMaxSize);
            Assert.Equal("0.00000001", result.Data.BaseMinSize);
            Assert.Equal("1000", result.Data.BaseMaxSize);
            Assert.Equal("Bitcoin", result.Data.BaseName);
            Assert.Equal("US Dollar", result.Data.QuoteName);
            Assert.True(result.Data.Watched);
            Assert.False(result.Data.IsDisabled);
            Assert.True(result.Data.IsNew);
            Assert.Equal("string", result.Data.Status);
            Assert.True(result.Data.CancelOnly);
            Assert.True(result.Data.LimitOnly);
            Assert.True(result.Data.PostOnly);
            Assert.False(result.Data.TradingDisabled);
            Assert.True(result.Data.AuctionMode);
            Assert.Equal("SPOT", result.Data.ProductType);
            Assert.Equal("USD", result.Data.QuoteCurrencyId);
            Assert.Equal("BTC", result.Data.BaseCurrencyId);
            Assert.Equal("140.22", result.Data.MidMarketPrice);
            Assert.Equal("BTC", result.Data.BaseDisplaySymbol);
            Assert.Equal("USD", result.Data.QuoteDisplaySymbol);
        }

        [Fact]
        public async Task GetProductAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Product> result;

            var productId = "TEST";

            var json = GetInvalidProductJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductAsync(productId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlParsingException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData(null)]
        public async Task GetProductAsync_NullOrWhitespaceProductId_ReturnsUnsuccessfulApiResponse(string productId)
        {
            //Arrange
            ApiResponse<Product> result;

            var json = GetProductJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductAsync(productId);
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
        public async Task GetProductsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Product> result;

            var productId = "TEST";

            var json = GetProductJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Products.GetProductAsync(productId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetProductAsync

        #region GetProductCandlesAsync

        [Fact]
        public async Task GetProductCandlesAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
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
        public async Task GetProductCandlesAsync_ValidRequestAndResponseJson_ResponseHasValidCandlesPage()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            var candle = result.Data.Candles.FirstOrDefault();

            //Assert
            Assert.NotNull(result.Data.Candles);
            Assert.NotEmpty(result.Data.Candles);
        }

        [Fact]
        public async Task GetProductCandlesAsync_ValidRequestAndResponseJson_ResponseHasValidCandles()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result.Data.Candles);
            Assert.Contains(result.Data.Candles, c => c.Start.Equals("1639508050", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task GetProductCandlesAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetInvalidCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.Equal(nameof(FlurlParsingException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData(null)]
        public async Task GetProductCandlesAsync_InvalidProductId_ReturnsUnsuccessfulApiResponse(string productId)
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.ProductIdRequired, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetProductCandlesAsync_InvalidStartDate_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.MinValue;
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.StartDateRequired, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetProductCandlesAsync_InvalidEndDate_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.MinValue;
            var granularity = CandleGranularity.OneMinute;

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }


            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.EndDateRequired, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetProductCandlesAsync_InvalidGranularity_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = "INVALID";

            var json = GetCandlesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.CandleGranularityInvalid, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetProductCandlesAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<CandlesPage> result;

            var productId = "TEST";
            var start = DateTimeOffset.UtcNow.AddDays(-2);
            var end = DateTimeOffset.UtcNow.AddDays(-1);
            var granularity = CandleGranularity.OneMinute;

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Products.GetProductCandlesAsync(productId, start, end, granularity);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetProductCandlesAsync

        #region GetMarketTradesAsync

        [Fact]
        public async Task GetMarketTradesAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var productId = "TEST";
            var limit = 1;

            var json = GetMarketTradesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
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
        public async Task GetMarketTradesAsync_ValidRequestAndResponseJson_ResponseHasValidTradesPage()
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var productId = "TEST";
            var limit = 1;

            var json = GetMarketTradesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
            }

            //Assert
            Assert.NotNull(result.Data.Trades);
            Assert.NotEmpty(result.Data.Trades);
            Assert.Contains(result.Data.Trades, t => t.TradeId.Equals("34b080bf-fcfd-445a-832b-46b5ddc65601", StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal("291.13", result.Data.BestBid);
            Assert.Equal("292.40", result.Data.BestAsk);
        }

        [Fact]
        public async Task GetMarketTradesAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var productId = "TEST";
            var limit = 1;

            var json = GetInvalidMarketTradesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.Equal(nameof(FlurlParsingException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetMarketTradesAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var productId = "TEST";

            var json = GetMarketTradesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData(null)]
        public async Task GetMarketTradesAsync_InvalidProductId_ReturnsUnsuccessfulApiResponse(string productId)
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var limit = 1;

            var json = GetMarketTradesListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(json);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
            Assert.Contains(ErrorMessages.ProductIdRequired, result.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public async Task GetMarketTradesAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TradesPage> result;

            var productId = "TEST";
            var limit = 1;

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Products.GetMarketTradesAsync(productId, limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetMarketTradesAsync

        #region Test Response Json

        private string GetProductsListJsonString()
        {
            var json =
            """
            {
                "products": [
                    {
                        "product_id": "BTC-USD",
                        "price": "140.21",
                        "price_percentage_change_24h": "9.43%",
                        "volume_24h": "1908432",
                        "volume_percentage_change_24h": "9.43%",
                        "base_increment": "0.00000001",
                        "quote_increment": "0.00000001",
                        "quote_min_size": "0.00000001",
                        "quote_max_size": "1000",
                        "base_min_size": "0.00000001",
                        "base_max_size": "1000",
                        "base_name": "Bitcoin",
                        "quote_name": "US Dollar",
                        "watched": true,
                        "is_disabled": false,
                        "new": true,
                        "status": "string",
                        "cancel_only": true,
                        "limit_only": true,
                        "post_only": true,
                        "trading_disabled": false,
                        "auction_mode": true,
                        "product_type": "SPOT",
                        "quote_currency_id": "USD",
                        "base_currency_id": "BTC",
                        "mid_market_price": "140.22",
                        "base_display_symbol": "BTC",
                        "quote_display_symbol": "USD"
                    }
                ],
                "num_products": 100
            }
            """;

            return json;
        }

        private string GetInvalidProductsListJsonString()
        {
            var json =
            """
            {
                "products": [
                    {
                        "product_id": "BTC-USD",
                        "price": "140.21",
                        "price_percentage_change_24h": "9.43%",
                        "volume_24h": "1908432",
                        "volume_percentage_change_24h": "9.43%",
                        "base_increment": "0.00000001",
                        "quote_increment": "0.00000001",
                        "quote_min_size": "0.00000001",
                        "quote_max_size": "1000",
                        "base_min_size": "0.00000001",
                        "base_max_size": "1000",
                        "base_name": "Bitcoin",
                        "quote_name": "US Dollar",
                        "watched": "INVALID",
                        "is_disabled": false,
                        "new": true,
                        "status": "string",
                        "cancel_only": true,
                        "limit_only": true,
                        "post_only": true,
                        "trading_disabled": false,
                        "auction_mode": true,
                        "product_type": "SPOT",
                        "quote_currency_id": "USD",
                        "base_currency_id": "BTC",
                        "mid_market_price": "140.22",
                        "base_display_symbol": "BTC",
                        "quote_display_symbol": "USD"
                    }
                ],
                "num_products": 100
            }
            """;

            return json;
        }

        private string GetProductJsonString()
        {
            var json =
            """
            {
                "product_id": "BTC-USD",
                "price": "140.21",
                "price_percentage_change_24h": "9.43%",
                "volume_24h": "1908432",
                "volume_percentage_change_24h": "9.43%",
                "base_increment": "0.00000001",
                "quote_increment": "0.00000001",
                "quote_min_size": "0.00000001",
                "quote_max_size": "1000",
                "base_min_size": "0.00000001",
                "base_max_size": "1000",
                "base_name": "Bitcoin",
                "quote_name": "US Dollar",
                "watched": true,
                "is_disabled": false,
                "new": true,
                "status": "string",
                "cancel_only": true,
                "limit_only": true,
                "post_only": true,
                "trading_disabled": false,
                "auction_mode": true,
                "product_type": "SPOT",
                "quote_currency_id": "USD",
                "base_currency_id": "BTC",
                "mid_market_price": "140.22",
                "base_display_symbol": "BTC",
                "quote_display_symbol": "USD"
            }
            """;

            return json;
        }

        private string GetInvalidProductJsonString()
        {
            var json =
            """
            {
                "product_id": "BTC-USD",
                "price": "140.21",
                "price_percentage_change_24h": "9.43%",
                "volume_24h": "1908432",
                "volume_percentage_change_24h": "9.43%",
                "base_increment": "0.00000001",
                "quote_increment": "0.00000001",
                "quote_min_size": "0.00000001",
                "quote_max_size": "1000",
                "base_min_size": "0.00000001",
                "base_max_size": "1000",
                "base_name": "Bitcoin",
                "quote_name": "US Dollar",
                "watched": "INVALID",
                "is_disabled": false,
                "new": true,
                "status": "string",
                "cancel_only": true,
                "limit_only": true,
                "post_only": true,
                "trading_disabled": false,
                "auction_mode": true,
                "product_type": "SPOT",
                "quote_currency_id": "USD",
                "base_currency_id": "BTC",
                "mid_market_price": "140.22",
                "base_display_symbol": "BTC",
                "quote_display_symbol": "USD"
            }
            """;

            return json;
        }

        private string GetCandlesListJsonString()
        {
            var json =
            """
            {
              "candles": [
                  {
                    "start": "1639508050",
                    "low": "140.21",
                    "high": "140.21",
                    "open": "140.21",
                    "close": "140.21",
                    "volume": "56437345"
                  }
              ]
            }
            """;

            return json;
        }

        private string GetInvalidCandlesListJsonString()
        {
            var json =
            """
            {
              "candles": 
                  {
                    "start": "1639508050",
                    "low": "140.21",
                    "high": "140.21",
                    "open": "140.21",
                    "close": "140.21",
                    "volume": "56437345"
                  }
            }
            """;

            return json;
        }

        private string GetMarketTradesListJsonString()
        {
            var json =
            """
            {
                "trades": [
                    {
                        "trade_id": "34b080bf-fcfd-445a-832b-46b5ddc65601",
                        "product_id": "BTC-USD",
                        "price": "140.91",
                        "size": "4",
                        "time": "2021-05-31T09:59:59Z",
                        "side": "UNKNOWN_ORDER_SIDE",
                        "bid": "291.13",
                        "ask": "292.40"
                    }
                ],
                "best_bid": "291.13",
                "best_ask": "292.40"
            }
            """;

            return json;
        }

        private string GetInvalidMarketTradesListJsonString()
        {
            var json =
            """
            {
                "trades": 
                    {
                        "trade_id": "34b080bf-fcfd-445a-832b-46b5ddc65601",
                        "product_id": "BTC-USD",
                        "price": "INVALID",
                        "size": "4",
                        "time": "2021-05-31T09:59:59Z",
                        "side": "UNKNOWN_ORDER_SIDE",
                        "bid": "291.13",
                        "ask": "292.40"
                    },
                "best_bid": "291.13",
                "best_ask": "292.40"
            }
            """;

            return json;
        }

        #endregion // Test Response Json
    }
}
