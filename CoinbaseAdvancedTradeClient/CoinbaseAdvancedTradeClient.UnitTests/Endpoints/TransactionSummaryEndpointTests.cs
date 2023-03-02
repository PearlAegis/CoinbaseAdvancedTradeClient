using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries;
using CoinbaseAdvancedTradeClient.Models.Config;
using Flurl.Http;
using Flurl.Http.Testing;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests.Endpoints
{
    public class TransactionSummaryEndpointTests
    {
        private readonly ICoinbaseAdvancedTradeApiClient _testClient;

        public TransactionSummaryEndpointTests()
        {
            var config = new ApiClientConfig()
            {
                ApiKey = "key",
                ApiSecret = "secret"
            };

            _testClient = new CoinbaseAdvancedTradeApiClient(config);
        }

        #region GetTransactionSummaryAsync

        [Fact]
        public async Task GetTransactionSummaryAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        public async Task GetTransactionSummaryAsync_ValidRequestAndResponseJson_ResponseHasValidTransactionSummaryValues()
        {
            //Arrange
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
            }

            //Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.FeeTier);
            Assert.NotNull(result.Data.MarginRate);
            Assert.NotNull(result.Data.GoodsAndServicesTax);
            Assert.Equal(1000, result.Data.TotalVolume);
            Assert.Equal(25, result.Data.TotalFees);
            Assert.Equal("<$10k", result.Data.FeeTier.PricingTier);
            Assert.Equal("0", result.Data.FeeTier.UsdFrom);
            Assert.Equal("10,000", result.Data.FeeTier.UsdTo);
            Assert.Equal("0.0010", result.Data.FeeTier.TakerFeeRate);
            Assert.Equal("0.0020", result.Data.FeeTier.MakerFeeRate);
            Assert.Equal("string", result.Data.MarginRate.Value);
            Assert.Equal("string", result.Data.GoodsAndServicesTax.Rate);
            Assert.Equal("INCLUSIVE", result.Data.GoodsAndServicesTax.Type);
            Assert.Equal(1000, result.Data.AdvancedTradeOnlyVolume);
            Assert.Equal(25, result.Data.AdvancedTradeOnlyFees);
            Assert.Equal(1000, result.Data.CoinbaseProVolume);
            Assert.Equal(25, result.Data.CoinbaseProFees);
        }

        [Fact]
        public async Task GetTransactionSummaryAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetInvalidTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        public async Task GetTransactionSummaryAsync_InvalidStartDate_ReturnsUnsuccessfulApiResponse()
        {
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.MinValue;
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        public async Task GetTransactionSummaryAsync_InvalidEndDate_ReturnsUnsuccessfulApiResponse()
        {
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.MinValue;
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        public async Task GetTransactionSummaryAsync_InvalidProductTypeValue_ReturnsUnsuccessfulApiResponse()
        {
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = "Invalid";

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        [InlineData(null)]
        [InlineData("")]
        public async Task GetTransactionSummaryAsync_NullOrEmptyProductType_ReturnsUnsuccessfulApiResponse(string productType)
        {
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(transactionSummaryJson);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
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
        public async Task GetTransactionSummaryAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<TransactionSummary> result;

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(7);
            var userNativeCurrency = "TEST";
            var productType = ProductTypes.Spot;

            var transactionSummaryJson = GetTransactionSummaryJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status:401);

                result = await _testClient.TransactionSummary.GetTransactionSummaryAsync(startDate, endDate, userNativeCurrency, productType);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetTransactionSummaryAsync

        #region Test Response Json

        private string GetTransactionSummaryJsonString()
        {
            var json =
            """
            {
                "total_volume": 1000,
                "total_fees": 25,
                "fee_tier": {
                    "pricing_tier": "<$10k",
                    "usd_from": "0",
                    "usd_to": "10,000",
                    "taker_fee_rate": "0.0010",
                    "maker_fee_rate": "0.0020"
                },
                "margin_rate": {
                    "value": "string"
                },
                "goods_and_services_tax": {
                    "rate": "string",
                    "type": "INCLUSIVE"
                },
                "advanced_trade_only_volume": 1000,
                "advanced_trade_only_fees": 25,
                "coinbase_pro_volume": 1000,
                "coinbase_pro_fees": 25
            }
            """;

            return json;
        }

        private string GetInvalidTransactionSummaryJsonString()
        {
            var json =
            """
            {
                "total_volume": "Invalid",
                "total_fees": 25,
                "fee_tier": {
                    "pricing_tier": "<$10k",
                    "usd_from": "0",
                    "usd_to": "10,000",
                    "taker_fee_rate": "0.0010",
                    "maker_fee_rate": "0.0020"
                },
                "margin_rate": {
                    "value": "string"
                },
                "goods_and_services_tax": {
                    "rate": "string",
                    "type": "INCLUSIVE"
                },
                "advanced_trade_only_volume": 1000,
                "advanced_trade_only_fees": 25,
                "coinbase_pro_volume": 1000,
                "coinbase_pro_fees": 25
            }
            """;

            return json;
        }

        #endregion // Test Response Json
    }
}
