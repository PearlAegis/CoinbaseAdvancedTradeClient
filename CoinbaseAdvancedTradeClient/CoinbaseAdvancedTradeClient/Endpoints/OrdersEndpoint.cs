﻿using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        Task<IList<object>> IOrdersEndpoint.GetListFills(object filterParameters)
        {
            throw new NotImplementedException();
        }

        Task<IList<object>> IOrdersEndpoint.GetListOrders(object filterParameters)
        {
            throw new NotImplementedException();
        }

        Task<object> IOrdersEndpoint.GetOrder(object filterParameters)
        {
            throw new NotImplementedException();
        }

        Task<object> IOrdersEndpoint.PostCancelOrders(string[] orderIds)
        {
            throw new NotImplementedException();
        }

        Task<object> IOrdersEndpoint.PostCreateOrder(object order)
        {
            throw new NotImplementedException();
        }
    }
}