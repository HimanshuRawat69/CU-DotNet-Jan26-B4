using SmartBank.TransactionService.DTOs;

namespace SmartBank.TransactionService.Services
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(TransactionDto dto);
    }
}