namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IOrdersEndpoint
    {
        //TODO Models
        Task<object> PostCreateOrder(object order);
        Task<object> PostCancelOrders(string[] orderIds);
        Task<IList<object>> GetListOrders(object filterParameters);
        Task<IList<object>> GetListFills(object filterParameters);
        Task<object> GetOrder(object filterParameters);
    }
}
