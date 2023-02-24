using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IProductsEndpoint
    {
        public IProductsEndpoint Products => this;

        public Task<IList<object>> GetListProducts(object filterParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<object>> GetMarketTrades(string productId, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<object>> GetProductCandles(string productId, object filterParameters)
        {
            throw new NotImplementedException();
        }
    }
}
