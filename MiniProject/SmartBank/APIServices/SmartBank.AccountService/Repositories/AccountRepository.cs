using Microsoft.EntityFrameworkCore;
using SmartBank.AccountService.Data;
using SmartBank.AccountService.Models;

namespace SmartBank.AccountService.Repositories
{
    /// <summary>
    /// BEGINNER NOTE: Repository is a pattern that handles all database operations for Accounts
    /// It separates database logic from business logic
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext _context;

        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// BEGINNER NOTE: Find an account by its ID (primary key) in the database
        /// </summary>
        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        /// <summary>
        /// BEGINNER NOTE: Get all accounts from the database
        /// </summary>
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        /// <summary>
        /// BEGINNER NOTE: Add a new account to the database
        /// SaveChangesAsync() commits the changes to SQL Server database
        /// </summary>
        public async Task AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();  // IMPORTANT: This saves to SQL Server!
        }

        /// <summary>
        /// BEGINNER NOTE: Update an existing account in the database (used for balance changes)
        /// SaveChangesAsync() commits the changes to SQL Server database
        /// </summary>
        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();  // IMPORTANT: This saves to SQL Server!
        }
    }
}
