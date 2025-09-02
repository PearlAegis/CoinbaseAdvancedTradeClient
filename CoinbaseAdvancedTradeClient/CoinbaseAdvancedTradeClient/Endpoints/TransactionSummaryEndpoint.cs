using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Extensions;
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

        async Task<ApiResponse<TransactionSummary>> ITransactionSummaryEndpoint.GetTransactionSummaryAsync(DateTimeOffset startDate, DateTimeOffset endDate, string userNativeCurrency, ProductType productType)
        {
            var response = new ApiResponse<TransactionSummary>();

            try
            {
                if (startDate.Equals(DateTimeOffset.MinValue)) throw new ArgumentException(ErrorMessages.StartDateRequired, nameof(startDate));
                if (endDate.Equals(DateTimeOffset.MinValue)) throw new ArgumentException(ErrorMessages.EndDateRequired, nameof(endDate));

                var transactionSummary = await _config.ApiBaseUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.TransactionSummaryEndpoint)
                    .SetQueryParam(RequestParameters.StartDate, startDate.ToUniversalTime())
                    .SetQueryParam(RequestParameters.EndDate, endDate.ToUniversalTime())
                    .SetQueryParam(RequestParameters.UserNativeCurrency, userNativeCurrency)
                    .SetQueryParam(RequestParameters.ProductType, productType.GetEnumMemberValue())
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
