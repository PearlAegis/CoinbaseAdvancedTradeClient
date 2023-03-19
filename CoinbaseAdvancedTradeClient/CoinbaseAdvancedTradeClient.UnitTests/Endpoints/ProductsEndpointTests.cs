using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
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


            //Act


            //Assert
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
    }
}
