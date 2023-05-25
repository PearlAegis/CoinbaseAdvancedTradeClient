using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;

namespace CoinbaseAdvancedTradeClient.Extensions
{
    internal static class OrderExtensions
    {
        internal static void BuildMarketIocConfiguration(this CreateOrderParameters order, decimal amount, OrderSide orderSide)
        {
            var marketIoc = new MarketIoc();

            if (orderSide.Equals(OrderSide.Buy))
            {
                marketIoc.QuoteSize = amount.ToString();
            }
            else
            {
                marketIoc.BaseSize = amount.ToString();
            }

            order.OrderConfiguration = new OrderConfiguration
            {
                MarketIoc = marketIoc
            };
        }

        internal static void BuildLimitGtcConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, bool postOnly)
        {
            var limitGtc = new LimitGtc();

            limitGtc.BaseSize = amount.ToString();
            limitGtc.LimitPrice = limitPrice.ToString();
            limitGtc.PostOnly = postOnly;

            order.OrderConfiguration = new OrderConfiguration
            {
                LimitGtc = limitGtc
            };
        }

        internal static void BuildLimitGtdConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, bool postOnly, DateTimeOffset endTime)
        {
            var limitGtd = new LimitGtd();

            limitGtd.BaseSize = amount.ToString();
            limitGtd.LimitPrice = limitPrice.ToString();
            limitGtd.PostOnly = postOnly;
            limitGtd.EndTime = endTime.DateTime.ToUniversalTime();

            order.OrderConfiguration = new OrderConfiguration
            {
                LimitGtd = limitGtd
            };
        }

        internal static void BuildStopLimitGtcConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection)
        {
            var stopLimitGtc = new StopLimitGtc();

            stopLimitGtc.BaseSize = amount.ToString();
            stopLimitGtc.LimitPrice = limitPrice.ToString();
            stopLimitGtc.StopPrice = stopPrice.ToString();
            stopLimitGtc.StopDirection = stopDirection;

            order.OrderConfiguration = new OrderConfiguration
            {
                StopLimitGtc = stopLimitGtc
            };
        }

        internal static void BuildStopLimitGtdConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTimeOffset endTime)
        {
            var stopLimitGtd = new StopLimitGtd();

            stopLimitGtd.BaseSize = amount.ToString();
            stopLimitGtd.LimitPrice = limitPrice.ToString();
            stopLimitGtd.StopPrice = stopPrice.ToString();
            stopLimitGtd.StopDirection = stopDirection;
            stopLimitGtd.EndTime = endTime.DateTime.ToUniversalTime();

            order.OrderConfiguration = new OrderConfiguration
            {
                StopLimitGtd = stopLimitGtd
            };
        }
    }
}
