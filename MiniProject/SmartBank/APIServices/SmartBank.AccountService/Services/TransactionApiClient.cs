using SmartBank.AccountService.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartBank.AccountService.Services
{
    /// <summary>
    /// BEGINNER NOTE: This client is responsible for calling the TransactionService API
    /// It sends HTTP requests to record transactions in the TransactionService database
    /// </summary>
    public class TransactionApiClient : ITransactionApiClient
    {
        private readonly HttpClient _client;

        public TransactionApiClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// BEGINNER NOTE: Send a transaction to the TransactionService to be saved
        /// This makes an HTTP POST request to https://localhost:7185/api/transaction
        /// </summary>
        public async Task CreateTransaction(TransactionCreateDto dto, string token)
        {
            // BEGINNER NOTE: Create the HTTP request with transaction data
            var request = new HttpRequestMessage(HttpMethod.Post, "api/transaction")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(dto),
                    Encoding.UTF8,
                    "application/json")
            };

            // BEGINNER NOTE: Attach the JWT token for authentication
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // BEGINNER NOTE: Send the request to TransactionService
            var response = await _client.SendAsync(request);

            // BEGINNER NOTE: If the request fails, throw an error with details
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new ApplicationException(
                    $"Transaction service failed: {response.StatusCode}, {error}");
            }
        }

        public async Task<List<TransactionDto>> GetTransactionsByAccountId(int accountId, string token)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/transaction/account/{accountId}");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new ApplicationException(
                    $"Failed to fetch transactions: {response.StatusCode}, {error}");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TransactionDto>>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}