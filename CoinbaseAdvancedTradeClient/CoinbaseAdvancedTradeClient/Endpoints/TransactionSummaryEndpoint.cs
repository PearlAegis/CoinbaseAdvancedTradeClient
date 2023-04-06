using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries;
using CoinbaseAdvancedTradeClient.Resources;
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
                if (startDate.Equals(DateTime.MinValue)) throw new ArgumentException(ErrorMessages.StartDateRequired, nameof(startDate));
                if (endDate.Equals(DateTime.MinValue)) throw new ArgumentException(ErrorMessages.EndDateRequired, nameof(endDate));
                if (string.IsNullOrWhiteSpace(productType)) throw new ArgumentNullException(nameof(productType), ErrorMessages.ProductTypeRequired);

                if (!ProductTypes.ProductTypeList.Contains(productType, StringComparer.InvariantCultureIgnoreCase)) throw new ArgumentException(ErrorMessages.ProductTypeInvalid, nameof(productType));

                var transactionSummary = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.TransactionSummaryEndpoint)
                    .SetQueryParam(RequestParameters.StartDate, startDate.ToUniversalTime())
                    .SetQueryParam(RequestParameters.EndDate, endDate.ToUniversalTime())
                    .SetQueryParam(RequestParameters.UserNativeCurrency, userNativeCurrency)
                    .SetQueryParam(RequestParameters.ProductType, productType)
                    .GetJsonAsync<TransactionSummary>();

                response.Data = transactionSummary;
                response.Success = true;
            }
            catch(Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }
    }
}
