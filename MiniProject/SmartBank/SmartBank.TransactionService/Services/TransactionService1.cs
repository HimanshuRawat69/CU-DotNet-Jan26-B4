using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Models;
using SmartBank.TransactionService.Repositories;

namespace SmartBank.TransactionService.Services
{
    /// <summary>
    /// BEGINNER NOTE: This service handles the business logic for transactions
    /// It receives transaction data and saves it to the database through the repository
    /// </summary>
    public class TransactionService1 : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService1(ITransactionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// BEGINNER NOTE: This method creates a transaction record
        /// It's called whenever money is deposited or withdrawn from an account
        /// </summary>
        public async Task CreateTransactionAsync(TransactionDto dto)
        {
            // BEGINNER NOTE: Convert the DTO (Data Transfer Object) to a Transaction entity
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Type = dto.Type,  // "Deposit" or "Withdraw"
                UserId = dto.UserId,
                AccountNumber = dto.AccountNumber
            };

            // BEGINNER NOTE: Save the transaction to SQL Server database
            await _repository.AddAsync(transaction);
        }
    }
}