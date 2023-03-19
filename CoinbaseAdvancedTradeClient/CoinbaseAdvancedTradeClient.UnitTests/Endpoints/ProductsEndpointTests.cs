using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var productType = "TEST";

            var productListJson = GetProductsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(productListJson);

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


            //Act


            //Assert
        }

        [Fact]
        public async Task GetListProductsAsync_ValidRequestAndResponseJson_ResponseHasValidProducts()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetListProductsAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetListProductsAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange


            //Act


            //Assert
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetListProductsAsync_InvalidOffsetRange_ReturnsUnsuccessfulApiResponse(int offset)
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetListProductsAsync_InvalidProductType_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }


        [Fact]
        public async Task GetListProductsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }

        #endregion // GetListProductsAsync

        #region GetProductAsync

        [Fact]
        public async Task GetProductAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetProductAsync_ValidRequestAndResponseJson_ResponseHasValidProductsPage()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetProductAsync_ValidRequestAndResponseJson_ResponseHasValidProducts()
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetProductAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetProductAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange


            //Act


            //Assert
        }

        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetProductAsync_InvalidOffsetRange_ReturnsUnsuccessfulApiResponse(int offset)
        {
            //Arrange


            //Act


            //Assert
        }

        [Fact]
        public async Task GetProductAsync_InvalidProductType_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }


        [Fact]
        public async Task GetProductsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange


            //Act


            //Assert
        }


        #endregion // GetProductAsync

        #region GetProductCandlesAsync



        #endregion // GetProductcandlesAsync

        #region GetMarketTradesAsync



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

        private string GetTradesListJsonString()
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

        private string GetInvalidTradesListJsonString()
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

        #endregion // Test Response Json
    }
}
