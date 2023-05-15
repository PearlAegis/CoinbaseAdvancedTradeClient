using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IOrdersEndpoint
    {
        Task<ApiResponse<CreateOrderResponse>> PostCreateOrderAsync(CreateOrderParameters createOrder, CancellationToken cancellationToken = default);
        Task<ApiResponse<CancelOrdersResponse>> PostCancelOrdersAsync(CancelOrdersParameters cancelOrders, CancellationToken cancellationToken = default);
        Task<ApiResponse<OrdersPage>> GetListOrdersAsync(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, OrderType? orderType = null, OrderSide? orderSide = null, string? cursor = null, ProductType? productType = null, OrderPlacementSource? orderPlacementSource = null);
        Task<ApiResponse<FillsPage>> GetListFillsAsync(string? orderId = null, string? productId = null, DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null);
        Task<ApiResponse<Order>> GetOrderAsync(string orderId);
        Task<ApiResponse<CreateOrderResponse>> CreateMarketOrderAsync(OrderSide orderSide, string productId, decimal amount, string clientOrderId = null, CancellationToken cancellationToken = default);
        Task<ApiResponse<CreateOrderResponse>> CreateLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide, string productId, decimal amount, decimal limitPrice, bool postOnly, DateTime endTime, string clientOrderId = null, CancellationToken cancellationToken = default);
        Task<ApiResponse<CreateOrderResponse>> CreateStopLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide, string productId, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTime endTime, string clientOrderId = null, CancellationToken cancellationToken = default);
    }
}
