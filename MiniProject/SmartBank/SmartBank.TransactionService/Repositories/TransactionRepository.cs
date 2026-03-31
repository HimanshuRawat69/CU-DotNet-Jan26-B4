using SmartBank.TransactionService.Data;
using SmartBank.TransactionService.Models;

namespace SmartBank.TransactionService.Repositories
{
    /// <summary>
    /// BEGINNER NOTE: This repository handles saving transaction records to the database
    /// Every deposit/withdrawal creates a transaction record for history tracking
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SmartBankTransactionServiceContext _context;

        public TransactionRepository(SmartBankTransactionServiceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// BEGINNER NOTE: Save a new transaction to the SQL Server database
        /// This creates a permanent record of the deposit or withdrawal
        /// </summary>
        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();  // IMPORTANT: This saves to SQL Server database (MySBTransactionDb)!
        }
    }
}