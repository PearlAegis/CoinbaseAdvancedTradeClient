using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        public Task<IList<object>> GetListFills(object filterParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<object>> GetListOrders(object filterParameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetOrder(object filterParameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> PostCancelOrders(string[] orderIds)
        {
            throw new NotImplementedException();
        }

        public Task<object> PostCreateOrder(object order)
        {
            throw new NotImplementedException();
        }
    }
}
