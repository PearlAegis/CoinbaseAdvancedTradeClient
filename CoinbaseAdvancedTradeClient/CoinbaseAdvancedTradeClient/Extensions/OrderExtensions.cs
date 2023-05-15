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
                marketIoc.QuoteSize = amount;
            }
            else
            {
                marketIoc.BaseSize = amount;
            }

            order.OrderConfiguration = new OrderConfiguration
            {
                MarketIoc = marketIoc
            };
        }

        internal static void BuildLimitGtcConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, bool postOnly)
        {
            var limitGtc = new LimitGtc();

            limitGtc.BaseSize = amount;
            limitGtc.LimitPrice = limitPrice;
            limitGtc.PostOnly = postOnly;

            order.OrderConfiguration = new OrderConfiguration
            {
                LimitGtc = limitGtc
            };
        }

        internal static void BuildLimitGtdConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, bool postOnly, DateTime endTime)
        {
            var limitGtd = new LimitGtd();

            limitGtd.BaseSize = amount;
            limitGtd.LimitPrice = limitPrice;
            limitGtd.PostOnly = postOnly;
            limitGtd.EndTime = endTime.ToUniversalTime();

            order.OrderConfiguration = new OrderConfiguration
            {
                LimitGtd = limitGtd
            };
        }

        internal static void BuildStopLimitGtcConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection)
        {
            var stopLimitGtc = new StopLimitGtc();

            stopLimitGtc.BaseSize = amount;
            stopLimitGtc.LimitPrice = limitPrice;
            stopLimitGtc.StopPrice = stopPrice;
            stopLimitGtc.StopDirection = stopDirection;

            order.OrderConfiguration = new OrderConfiguration
            {
                StopLimitGtc = stopLimitGtc
            };
        }

        internal static void BuildStopLimitGtdConfiguration(this CreateOrderParameters order, decimal amount, decimal limitPrice, decimal stopPrice, StopDirection stopDirection, DateTime endTime)
        {
            var stopLimitGtd = new StopLimitGtd();

            stopLimitGtd.BaseSize = amount;
            stopLimitGtd.LimitPrice = limitPrice;
            stopLimitGtd.StopPrice = stopPrice;
            stopLimitGtd.StopDirection = stopDirection;
            stopLimitGtd.EndTime = endTime.ToUniversalTime();

            order.OrderConfiguration = new OrderConfiguration
            {
                StopLimitGtd = stopLimitGtd
            };
        }
    }
}
