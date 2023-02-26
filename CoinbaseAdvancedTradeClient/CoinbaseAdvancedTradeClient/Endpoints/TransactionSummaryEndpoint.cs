using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : ITransactionSummaryEndpoint
    {
        public ITransactionSummaryEndpoint TransactionSummary => this;

        async Task<ApiResponse<TransactionSummary>> ITransactionSummaryEndpoint.GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, string productType)
        {
            var response = new ApiResponse<TransactionSummary>();

            try
            {
                //TODO Parameter validation
                //TODO Parameter constants

                var transactionSummary = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.TransactionSummaryEndpoint)
                    .SetQueryParam("start_date", startDate)
                    .SetQueryParam("end_date", endDate)
                    .SetQueryParam("user_native_currency", userNativeCurrency)
                    .SetQueryParam("product_type", productType)
                    .GetJsonAsync<TransactionSummary>();

                response.Data = transactionSummary;
                response.Success = true;
            }
            catch(Exception ex)
            {
                await HandleExceptionResponse(ex, response);
            }

            return response;
        }
    }
}
