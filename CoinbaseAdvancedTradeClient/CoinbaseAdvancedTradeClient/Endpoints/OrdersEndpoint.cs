using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        async Task<ApiResponse<FillsPage>> IOrdersEndpoint.GetListFills(string? orderId = null, string? productId = null, DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null)
        {
            var response = new ApiResponse<FillsPage>();

            try
            {
                //TODO Parameter Validation
                var fillsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalFillsEndpoint)
                    .SetQueryParam(RequestParameters.OrderId, orderId)
                    .SetQueryParam(RequestParameters.ProductId, productId)
                    .SetQueryParam(RequestParameters.StartSequenceTimestamp, start)
                    .SetQueryParam(RequestParameters.EndSequenceTimestamp, end)
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.Cursor, cursor)
                    .GetJsonAsync<FillsPage>();

                response.Data = fillsPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
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
