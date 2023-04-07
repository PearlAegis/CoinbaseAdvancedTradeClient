using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IOrdersEndpoint
    {
        Task<ApiResponse<CreateOrderResponse>> PostCreateOrderAsync(CreateOrderParameters order, CancellationToken cancellationToken = default);
        Task<ApiResponse<Order>> PostCancelOrdersAsync(string[] orderIds, CancellationToken cancellationToken = default);
        Task<ApiResponse<OrdersPage>> GetListOrdersAsync(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, string? orderType = null, string? orderSide = null, string? cursor = null, string? productType = null, string? orderPlacementSource = null);
        Task<ApiResponse<FillsPage>> GetListFillsAsync(string? orderId = null, string? productId = null, DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null);
        Task<ApiResponse<Order>> GetOrderAsync(string orderId);
    }
}
