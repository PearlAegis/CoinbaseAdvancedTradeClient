using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IOrdersEndpoint
    {
        //TODO Models
        Task<object> PostCreateOrder(object order);
        Task<object> PostCancelOrders(string[] orderIds);
        Task<IList<object>> GetListOrders(object filterParameters);
        Task<ApiResponse<FillsPage>> GetListFills(string? orderId = null, string? productId = null, DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null);
        Task<object> GetOrder(object filterParameters);
    }
}
