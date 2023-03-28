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

        }

        [Fact]
        public async Task GetListOrdersAsync_ValidRequestAndResponseJson_ResultHasValidOrdersPage()
        {

        }

        [Fact]
        public async Task GetListOrdersAsync_ValidRequestAndResponseJson_ResponseHasValidOrders()
        {

        }

        #endregion // GetListOrdersAsync

        #region GetListFillsAsync


        #endregion // GetListFillsAsync

        #region GetOrderAsync


        #endregion // GetOrderAsync
    }
}
