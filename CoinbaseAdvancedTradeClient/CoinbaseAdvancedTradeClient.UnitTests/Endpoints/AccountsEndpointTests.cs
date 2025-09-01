using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Accounts;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl.Http;
using Flurl.Http.Testing;
using System.Globalization;
using Xunit;

namespace CoinbaseAdvancedTradeClient.UnitTests.Endpoints
{
    public class AccountsEndpointTests
    {
        private readonly ICoinbaseAdvancedTradeApiClient _testClient;

        public AccountsEndpointTests()
        {
            var config = new SecretApiKeyConfig()
            {
                KeyName = "key",
                KeySecret = TestHelpers.TestConfigHelper.GenerateTestKeySecret()
            };

            _testClient = new CoinbaseAdvancedTradeApiClient(config);
        }

        #region GetListAccountsAsync

        [Fact]
        public async Task GetListAccountsAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var accountsListJson = GetAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountsListJson);
                
                result = await _testClient.Accounts.GetListAccountsAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetListAccountsAsync_ValidRequestAndResponseJson_ResponseHasValidAccountsPage()
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var accountsListJson = GetAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountsListJson);

                result = await _testClient.Accounts.GetListAccountsAsync();
            }

            //Assert
            Assert.Null(result.Data.Account);
            Assert.NotNull(result.Data.Accounts);
            Assert.Equal(2, result.Data.Size);
            Assert.True(result.Data.HasNext);
            Assert.Equal("789100", result.Data.Cursor);
        }

        [Fact]
        public async Task GetListAccountsAsync_ValidRequestAndResponseJson_ResponseHasValidAccounts()
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var accountsListJson = GetAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountsListJson);

                result = await _testClient.Accounts.GetListAccountsAsync();
            }

            //Assert
            Assert.NotNull(result.Data.Accounts);
            Assert.Contains(result.Data.Accounts, a => a.Currency.Equals("BTC", StringComparison.InvariantCultureIgnoreCase));
            Assert.Contains(result.Data.Accounts, a => a.Currency.Equals("ETH", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async Task GetListAccountsAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var invalidJson = GetInvalildAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Accounts.GetListAccountsAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }


        [Theory]
        [InlineData(251)]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task GetListAccountsAsync_InvalidLimitRange_ReturnsUnsuccessfulApiResponse(int limit)
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var accountsListJson = GetAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountsListJson);

                result = await _testClient.Accounts.GetListAccountsAsync(limit);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetListAccountsAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<AccountsPage> result;

            var accountsListJson = GetAccountsListJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Accounts.GetListAccountsAsync();
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetListAccountsAsync

        #region GetAccountAsync

        [Fact]
        public async Task GetAccountAsync_ValidRequestAndResponseJson_ReturnsSuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Account> result;

            var accountId = "test";
            var accountJson = GetAccountJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountJson);

                result = await _testClient.Accounts.GetAccountAsync(accountId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
            Assert.Null(result.ExceptionType);
            Assert.Null(result.ExceptionMessage);
            Assert.Null(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetAccountsAsync_ValidRequestAndResponseJson_ResponseHasValidAccountValues()
        {
            //Arrange
            ApiResponse<Account> result;

            var accountId = "test";
            var accountJson = GetAccountJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountJson);

                result = await _testClient.Accounts.GetAccountAsync(accountId);
            }

            //Assert
            Assert.NotNull(result.Data);
            Assert.Equal("8bfc20d7-f7c6-4422-bf07-8243ca4169fe", result.Data.Id);
            Assert.Equal("BTC Wallet", result.Data.Name);
            Assert.Equal("BTC", result.Data.Currency);
            Assert.Equal(1.23m, result.Data.AvailableBalance.Value);
            Assert.Equal("BTC", result.Data.AvailableBalance.Currency);
            Assert.False(result.Data.Default);
            Assert.True(result.Data.Active);
            Assert.Equal("2021-05-31T09:59:59", result.Data.CreatedAt.Value.ToString("s", new CultureInfo("en-US")));
            Assert.Equal("2021-05-31T09:59:59", result.Data.UpdatedAt.Value.ToString("s", new CultureInfo("en-US")));
            Assert.Equal("2021-05-31T09:59:59", result.Data.DeletedAt.Value.ToString("s", new CultureInfo("en-US")));
            Assert.Equal("ACCOUNT_TYPE_UNSPECIFIED", result.Data.Type);
            Assert.True(result.Data.Ready);
            Assert.Equal(1.23m, result.Data.Hold.Value);
            Assert.Equal("BTC", result.Data.Hold.Currency);
        }

        [Fact]
        public async Task GetAccountAsync_InvalidResponseJson_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Account> result;

            var accountId = "test";
            var invalidJson = GetInvalidAccountJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(invalidJson);

                result = await _testClient.Accounts.GetAccountAsync(accountId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotNull(result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData(null)]
        public async Task GetAccountAsync_NullOrWhitespaceAccountId_ReturnsUnsuccessfulApiResponse(string accountId)
        {
            //Arrange
            ApiResponse<Account> result;

            var accountJson = GetAccountJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(accountJson);

                result = await _testClient.Accounts.GetAccountAsync(accountId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(ArgumentNullException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        [Fact]
        public async Task GetAccountAsync_UnauthorizedResponseStatus_ReturnsUnsuccessfulApiResponse()
        {
            //Arrange
            ApiResponse<Account> result;

            var accountId = "test";
            var accountJson = GetAccountJsonString();

            //Act
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 401);

                result = await _testClient.Accounts.GetAccountAsync(accountId);
            }

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(nameof(FlurlHttpException), result.ExceptionType);
            Assert.NotNull(result.ExceptionMessage);
            Assert.NotNull(result.ExceptionDetails);
        }

        #endregion // GetAccountAsync

        #region Test Response Json

        private string GetAccountsListJsonString()
        {
            var json =
            """
            {
                "accounts": [
                    {
                        "uuid": "8bfc20d7-f7c6-4422-bf07-8243ca4169fe",
                        "name": "BTC Wallet",
                        "currency": "BTC",
                        "available_balance": {
                            "value": "1.23",
                            "currency": "BTC"
                        },
                        "default": false,
                        "active": true,
                        "created_at": "2021-05-31T09:59:59Z",
                        "updated_at": "2021-05-31T09:59:59Z",
                        "deleted_at": "2021-05-31T09:59:59Z",
                        "type": "ACCOUNT_TYPE_UNSPECIFIED",
                        "ready": true,
                        "hold": {
                            "value": "1.23",
                            "currency": "BTC"
                        }
                    },
                    {
                        "uuid": "9bfc20d7-e7c5-3421-af06-9243ca4169ff",
                        "name": "ETH Wallet",
                        "currency": "ETH",
                        "available_balance": {
                            "value": "1.23",
                            "currency": "ETH"
                        },
                        "default": false,
                        "active": true,
                        "created_at": "2021-05-31T09:59:59Z",
                        "updated_at": "2021-05-31T09:59:59Z",
                        "deleted_at": "2021-05-31T09:59:59Z",
                        "type": "ACCOUNT_TYPE_UNSPECIFIED",
                        "ready": true,
                        "hold": {
                            "value": "1.23",
                            "currency": "ETH"
                        }
                    }
                ],
                "has_next": true,
                "cursor": "789100",
                "size": "2"
            }
            """;

            return json;
        }


        private string GetInvalildAccountsListJsonString()
        {
            var json =
            """
            {
                "accounts": [
                    {
                        "uuid": "8bfc20d7-f7c6-4422-bf07-8243ca4169fe",
                        "name": "BTC Wallet",
                        "currency": "BTC",
                        "available_balance": {
                            "value": "1.23",
                            "currency": "BTC"
                        },
                        "default": false,
                        "active": true,
                        "created_at": "2021-05-31T09:59:59Z",
                        "updated_at": "2021-05-31T09:59:59Z",
                        "deleted_at": "2021-05-31T09:59:59Z",
                        "type": "ACCOUNT_TYPE_UNSPECIFIED",
                        "ready": true,
                        "hold": {
                            "value": "1.23",
                            "currency": "BTC"
                        }
                    },
                    {
                        "uuid": "9bfc20d7-e7c5-3421-af06-9243ca4169ff",
                        "name": "ETH Wallet",
                        "currency": "ETH",
                        "available_balance": {
                            "value": "1.23",
                            "currency": "ETH"
                        },
                        "default": false,
                        "active": true,
                        "created_at": "2021-05-31T09:59:59Z",
                        "updated_at": "2021-05-31T09:59:59Z",
                        "deleted_at": "2021-05-31T09:59:59Z",
                        "type": "ACCOUNT_TYPE_UNSPECIFIED",
                        "ready": true,
                        "hold": {
                            "value": "1.23",
                            "currency": "ETH"
                        }
                    }
                ],
                "has_next": true,
                "cursor": "789100",
                "size": "invalid"
            }
            """;

            return json;
        }

        private string GetAccountJsonString()
        {
            var json =
            """
            {
                "account": {
                    "uuid": "8bfc20d7-f7c6-4422-bf07-8243ca4169fe",
                    "name": "BTC Wallet",
                    "currency": "BTC",
                    "available_balance": {
                        "value": "1.23",
                        "currency": "BTC"
                    },
                    "default": false,
                    "active": true,
                    "created_at": "2021-05-31T09:59:59Z",
                    "updated_at": "2021-05-31T09:59:59Z",
                    "deleted_at": "2021-05-31T09:59:59Z",
                    "type": "ACCOUNT_TYPE_UNSPECIFIED",
                    "ready": true,
                    "hold": {
                        "value": "1.23",
                        "currency": "BTC"
                    }
                }
            }
            """;

            return json;
        }

        private string GetInvalidAccountJsonString()
        {
            var json =
            """
            {
                "account": {
                    "uuid": "8bfc20d7-f7c6-4422-bf07-8243ca4169fe",
                    "name": "BTC Wallet",
                    "currency": "BTC",
                    "available_balance": {
                        "value": "1.23",
                        "currency": "BTC"
                    },
                    "default": false,
                    "active": "invalid",
                    "created_at": "2021-05-31T09:59:59Z",
                    "updated_at": "2021-05-31T09:59:59Z",
                    "deleted_at": "2021-05-31T09:59:59Z",
                    "type": "ACCOUNT_TYPE_UNSPECIFIED",
                    "ready": true,
                    "hold": {
                        "value": "1.23",
                        "currency": "BTC"
                    }
                }
            }
            """;

            return json;
        }

        #endregion // Test Response Json
    }
}
