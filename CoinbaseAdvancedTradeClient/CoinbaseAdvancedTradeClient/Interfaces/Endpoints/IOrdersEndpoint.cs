using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IOrdersEndpoint
    {
        //TODO Models
        Task<object> PostCreateOrder(object order);
        Task<object> PostCancelOrders(string[] orderIds);
        Task<ApiResponse<OrdersPage>> GetListOrders(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, string? orderType = null, string? orderSide = null, string? cursor = null, string? productType = null, string? orderPlacementSource = null);
        Task<ApiResponse<FillsPage>> GetListFills(string? orderId = null, string? productId = null, DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null);
        Task<object> GetOrder(object filterParameters);
    }
}
