﻿using CoinbaseAdvancedTradeClient.Constants;
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

                ValidateCreateOrderConfiguration(createOrder);

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

        #region Create Order Helper Methods

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateMarketOrderAsync(OrderSide orderSide,
            string productId, decimal amount,
            string clientOrderId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentException(ErrorMessages.ProductIdRequired, nameof(productId));
            if (amount <= 0) throw new ArgumentException(nameof(amount), "Amount must be greater than zero."); //TODO Add to error messages

            if (string.IsNullOrWhiteSpace(clientOrderId))
            {
                clientOrderId = Guid.NewGuid().ToString();
            }

            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            createOrderParameters.OrderConfiguration = BuildMarketIocConfiguration(orderSide, amount);

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide,
            string productId, decimal amount, decimal limitPrice, DateTime endTime, bool postOnly,
            string clientOrderId, CancellationToken cancellationToken = default)
        {
            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            if (timeInForce.Equals(TimeInForce.GoodTilCancelled))
            {
                createOrderParameters.OrderConfiguration = BuildLimitGtcConfiguration(amount, limitPrice, postOnly);
            }
            else
            {
                createOrderParameters.OrderConfiguration = BuildLimitGtdConfiguration(amount, limitPrice, postOnly, endTime);
            }

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        async Task<ApiResponse<CreateOrderResponse>> IOrdersEndpoint.CreateStopLimitOrderAsync(TimeInForce timeInForce, OrderSide orderSide,
            string productId, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTime endTime,
            string clientOrderId, CancellationToken cancellationToken = default)
        {
            var createOrderParameters = new CreateOrderParameters
            {
                ClientOrderId = clientOrderId,
                ProductId = productId,
                Side = orderSide,
            };

            if (timeInForce.Equals(TimeInForce.GoodTilCancelled))
            {
                createOrderParameters.OrderConfiguration = BuildStopLimitGtcConfiguration(orderSide, amount, limitPrice, stopPrice, stopDirection);
            }
            else
            {
                createOrderParameters.OrderConfiguration = BuildStopLimitGtdConfiguration(orderSide, amount, limitPrice, stopPrice, stopDirection, endTime);
            }

            return await Orders.PostCreateOrderAsync(createOrderParameters, cancellationToken);
        }

        #endregion // Create Order Helper Methods

        #region Build Order Configuration Methods

        private OrderConfiguration BuildMarketIocConfiguration(OrderSide orderSide, decimal amount)
        {
            var orderConfiguration = new OrderConfiguration();
            var marketIoc = new MarketIoc();

            if (orderSide.Equals(OrderSide.Buy))
            {
                marketIoc.QuoteSize = amount.ToString();
            }
            else
            {
                marketIoc.BaseSize = amount.ToString();
            }

            orderConfiguration.MarketIoc = marketIoc;

            return orderConfiguration;
        }

        private OrderConfiguration BuildLimitGtcConfiguration(decimal amount, decimal limitPrice, bool postOnly)
        {
            var orderConfiguration = new OrderConfiguration();
            var limitGtc = new LimitGtc();

            limitGtc.BaseSize = amount.ToString();
            limitGtc.LimitPrice = limitPrice.ToString();
            limitGtc.PostOnly = postOnly;

            orderConfiguration.LimitGtc = limitGtc;

            return orderConfiguration;
        }

        private OrderConfiguration BuildLimitGtdConfiguration(decimal amount, decimal limitPrice, bool postOnly, DateTime endTime)
        {
            var orderConfiguration = new OrderConfiguration();
            var limitGtd = new LimitGtd();

            limitGtd.BaseSize = amount.ToString();
            limitGtd.LimitPrice = limitPrice.ToString();
            limitGtd.PostOnly = postOnly;
            limitGtd.EndTime = endTime;

            orderConfiguration.LimitGtd = limitGtd;

            return orderConfiguration;
        }

        private OrderConfiguration BuildStopLimitGtcConfiguration(OrderSide orderSide, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection)
        {
            var orderConfiguration = new OrderConfiguration();
            var stopLimitGtc = new StopLimitGtc();

            stopLimitGtc.BaseSize = amount.ToString();
            stopLimitGtc.LimitPrice= limitPrice.ToString();
            stopLimitGtc.StopPrice = stopPrice.ToString();
            stopLimitGtc.StopDirection = stopDirection;

            return orderConfiguration;
        }

        private OrderConfiguration BuildStopLimitGtdConfiguration(OrderSide orderSide, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTime endTime)
        {
            var orderConfiguration = new OrderConfiguration();
            var stopLimitGtd = new StopLimitGtd();

            stopLimitGtd.BaseSize = amount.ToString();
            stopLimitGtd.LimitPrice = limitPrice.ToString();
            stopLimitGtd.StopPrice = stopPrice.ToString();
            stopLimitGtd.StopDirection = stopDirection;
            stopLimitGtd.EndTime = endTime;

            return orderConfiguration;
        }

        #endregion // Build Order Configuration Methods

        #region Validate Order Configuration Methods

        private void ValidateCreateOrderConfiguration(CreateOrderParameters createOrder)
        {
            if (string.IsNullOrWhiteSpace(createOrder.ClientOrderId))
            {
                createOrder.ClientOrderId = Guid.NewGuid().ToString();
            }

            if (createOrder.OrderConfiguration.MarketIoc != null)
            {
                ValidateMarketOrder(createOrder.OrderConfiguration.MarketIoc);
            }

            if (createOrder.OrderConfiguration.LimitGtc != null)
            {
                ValidateLimitGtcOrder(createOrder.OrderConfiguration.LimitGtc);
            }

            if (createOrder.OrderConfiguration.LimitGtd != null)
            {
                ValidateLimitGtdOrder(createOrder.OrderConfiguration.LimitGtd);
            }

            if (createOrder.OrderConfiguration.StopLimitGtc != null)
            {
                ValidateStopLimitGtcOrder(createOrder.OrderConfiguration.StopLimitGtc, createOrder.Side);
            }

            if (createOrder.OrderConfiguration.StopLimitGtd != null)
            {
                ValidateStopLimitGtdOrder(createOrder.OrderConfiguration.StopLimitGtd, createOrder.Side);
            }
        }

        private void ValidateMarketOrder(MarketIoc marketOrder)
        {

        }

        private void ValidateLimitGtcOrder(LimitGtc limitGtc)
        {

        }

        private void ValidateLimitGtdOrder(LimitGtd limitGtd)
        {
            if (limitGtd.EndTime != null)
            {
                limitGtd.EndTime = limitGtd.EndTime.Value.ToUniversalTime();
            }
        }

        private void ValidateStopLimitGtcOrder(StopLimitGtc stopLimitGtc, OrderSide orderSide)
        {
            ValidateStopLimitPricing(orderSide, stopLimitGtc);
        }

        private void ValidateStopLimitGtdOrder(StopLimitGtd stopLimitGtd, OrderSide orderSide)
        {
            ValidateStopLimitPricing(orderSide, stopLimitGtd);

            if (stopLimitGtd.EndTime != null)
            {
                stopLimitGtd.EndTime = stopLimitGtd.EndTime.Value.ToUniversalTime();
            }
        }

        private void ValidateStopLimitPricing(OrderSide orderSide, StopLimitGtc stopLimit)
        {
            var limitParsed = decimal.TryParse(stopLimit.LimitPrice, out decimal limitPrice);
            var stopParsed = decimal.TryParse(stopLimit.StopPrice, out decimal stopPrice);

            if (!limitParsed) throw new ArgumentException("Invalid limit price"); //TODO error message
            if (!stopParsed) throw new ArgumentException("Invalid stop limit price"); //TODO error message

            if (orderSide.Equals(OrderSide.Buy))
            {
                if (limitPrice > stopPrice) throw new ArgumentException("Pricing invalid"); //TODO error message
            }
            else
            {
                if (limitPrice < stopPrice) throw new ArgumentException("Pricing invalid"); //TODO error message
            }
        }

        #endregion // Validate Order Configuration Methods

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
    }
}
