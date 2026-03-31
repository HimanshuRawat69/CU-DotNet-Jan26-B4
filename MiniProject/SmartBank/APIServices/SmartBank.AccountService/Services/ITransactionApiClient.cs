using SmartBank.AccountService.DTOs;

namespace SmartBank.AccountService.Services
{
    public interface ITransactionApiClient
    {
        Task CreateTransaction(TransactionCreateDto dto, string token);
        Task<List<TransactionDto>> GetTransactionsByAccountId(int accountId, string token);
    }
}
