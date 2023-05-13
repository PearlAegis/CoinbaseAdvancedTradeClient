using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Extensions;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Pages;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        #region GET

        async Task<ApiResponse<FillsPage>> IOrdersEndpoint.GetListFillsAsync(string? orderId = null, string? productId = null,
            DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null)
        {
            var response = new ApiResponse<FillsPage>();

            try
            {
                if (limit != null && limit != 0 && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                if (start.Equals(DateTimeOffset.MinValue)) start = null;
                if (end.Equals(DateTimeOffset.MinValue)) end = null;

                var fillsPage = await _config.ApiUrl
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

        async Task<ApiResponse<OrdersPage>> IOrdersEndpoint.GetListOrdersAsync(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null,
            DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, string? orderType = null, OrderSide? orderSide = null,
            string? cursor = null, string? productType = null, string? orderPlacementSource = null)
        {
            var response = new ApiResponse<OrdersPage>();

            try
            {
                if (!string.IsNullOrWhiteSpace(productType) && !ProductTypes.ProductTypeList.Contains(productType)) throw new ArgumentException(ErrorMessages.ProductTypeInvalid, nameof(productType));
                if (!string.IsNullOrWhiteSpace(orderType) && !OrderTypes.OrderTypeList.Contains(orderType)) throw new ArgumentException(ErrorMessages.OrderTypeInvalid, nameof(orderType));
                if (!string.IsNullOrWhiteSpace(orderPlacementSource) && !OrderTypes.OrderTypeList.Contains(orderPlacementSource)) throw new ArgumentException(ErrorMessages.OrderPlacementSourceInvalid, nameof(orderPlacementSource));
                if (limit != null && limit != 0 && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                if (orderStatuses != null && !orderStatuses.Any()) orderStatuses = null;
                if (startDate.Equals(DateTimeOffset.MinValue)) startDate = null;
                if (endDate.Equals(DateTimeOffset.MinValue)) endDate = null;

                var ordersPage = await _config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalBatchEndpoint)
                    .SetQueryParam(RequestParameters.ProductId, productId)
                    .SetQueryParam(RequestParameters.OrderStatus, orderStatuses?.ToArray())
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.StartDate, startDate)
                    .SetQueryParam(RequestParameters.EndDate, endDate)
                    .SetQueryParam(RequestParameters.UserNativeCurrency, userNativeCurrency)
                    .SetQueryParam(RequestParameters.OrderType, orderType)
                    .SetQueryParam(RequestParameters.OrderSide, orderSide?.GetEnumMemberValue())
                    .SetQueryParam(RequestParameters.Cursor, cursor)
                    .SetQueryParam(RequestParameters.ProductType, productType)
                    .SetQueryParam(RequestParameters.OrderPlacementSource, orderPlacementSource)
                    .GetJsonAsync<OrdersPage>();

                response.Data = ordersPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Order>> IOrdersEndpoint.GetOrderAsync(string orderId)
        {
            var response = new ApiResponse<Order>();

            try
            {
                if (string.IsNullOrWhiteSpace(orderId)) throw new ArgumentNullException(nameof(orderId), ErrorMessages.OrderIdRequired);

                var ordersPage = await _config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalEndpoint)
                    .AppendPathSegment(orderId)
                    .GetJsonAsync<OrdersPage>();

                response.Data = ordersPage.Order;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        #endregion // GET

        #region POST

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.PostCreateOrderAsync(CreateOrderParameters createOrder, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<CreateOrderResponse>();

            try
            {
                if (createOrder == null) throw new ArgumentNullException(nameof(createOrder), ErrorMessages.OrderParametersRequired);
                if (string.IsNullOrWhiteSpace(createOrder.ProductId)) throw new ArgumentException(ErrorMessages.ProductIdRequired, nameof(createOrder.ProductId));
                if (createOrder.OrderConfiguration == null) throw new ArgumentException(ErrorMessages.OrderConfigurationInvalid, nameof(createOrder.OrderConfiguration));

                if (string.IsNullOrEmpty(createOrder.ClientOrderId))
                {
                    createOrder.ClientOrderId = Guid.NewGuid().ToString();
                }

                var createOrderResponse = await _config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersEndpoint)
                    .PostJsonAsync(createOrder, cancellationToken)
                    .ReceiveJson<CreateOrderResponse>();

                response.Data = createOrderResponse;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<CancelOrdersResponse>> IOrdersEndpoint.PostCancelOrdersAsync(CancelOrdersParameters cancelOrders, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<CancelOrdersResponse>();

            try
            {
                if (cancelOrders == null) throw new ArgumentNullException(nameof(cancelOrders), ErrorMessages.OrderParametersRequired);
                if (cancelOrders.OrderIds == null || !cancelOrders.OrderIds.Any()) throw new ArgumentNullException(nameof(cancelOrders), ErrorMessages.OrderIdRequired);

                var cancelOrderResponse = await _config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersBatchCancelEndpoint)
                    .PostJsonAsync(cancelOrders, cancellationToken)
                    .ReceiveJson<CancelOrdersResponse>();

                response.Data = cancelOrderResponse;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        #endregion // POST

        #region Create Order Helper Methods

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateMarketOrderAsync(OrderSide orderSide,
            string productId, decimal amount,
            string clientOrderId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);
            if (amount <= 0) throw new ArgumentException(ErrorMessages.AmountParameterRange, nameof(amount));

            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            createOrderParameters.BuildMarketIocConfiguration(amount, orderSide);

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide,
            string productId, decimal amount, decimal limitPrice, bool postOnly, DateTime endTime,
            string clientOrderId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);
            if (amount <= 0) throw new ArgumentException(ErrorMessages.AmountParameterRange, nameof(amount));
            if (limitPrice <= 0) throw new ArgumentException(ErrorMessages.LimitPriceParameterRange, nameof(limitPrice));

            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            if (timeInForce.Equals(TimeInForce.GoodTilCancelled))
            {
                createOrderParameters.BuildLimitGtcConfiguration(amount, limitPrice, postOnly);
            }
            else
            {
                createOrderParameters.BuildLimitGtdConfiguration(amount, limitPrice, postOnly, endTime);
            }

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateStopLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide,
            string productId, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTime endTime,
            string clientOrderId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);
            if (amount <= 0) throw new ArgumentException(ErrorMessages.AmountParameterRange, nameof(amount));
            if (limitPrice <= 0) throw new ArgumentException(ErrorMessages.LimitPriceParameterRange, nameof(limitPrice));
            if (stopPrice <= 0) throw new ArgumentException(ErrorMessages.StopPriceParameterRange, nameof(stopPrice));

            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            if (timeInForce.Equals(TimeInForce.GoodTilCancelled))
            {
                createOrderParameters.BuildStopLimitGtcConfiguration(amount, limitPrice, stopPrice, stopDirection);
            }
            else
            {
                createOrderParameters.BuildStopLimitGtdConfiguration(amount, limitPrice, stopPrice, stopDirection, endTime);
            }

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        #endregion // Create Order Helper Methods
    }
}
