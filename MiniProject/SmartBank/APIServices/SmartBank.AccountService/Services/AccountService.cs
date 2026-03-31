using SmartBank.AccountService.Data;
using SmartBank.AccountService.DTOs;
using SmartBank.AccountService.Exceptions;
using SmartBank.AccountService.Models;
using SmartBank.AccountService.Repositories;

namespace SmartBank.AccountService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly AccountDbContext _context;
        private readonly ITransactionApiClient _transactionClient;

        public AccountService(IAccountRepository repo, AccountDbContext context,
            ITransactionApiClient transactionClient)
        {
            _repo = repo;
            _context = context;
            _transactionClient = transactionClient;
        }

        /// <summary>
        /// BEGINNER NOTE: This method creates a new bank account for a user
        /// It generates a unique account number and sets the initial balance to 0
        /// </summary>
        public async Task<Account> CreateAccount(string userId)
        {
            // BEGINNER NOTE: Create a new Account object with initial values
            var account = new Account
            {
                UserId = userId,  // Link this account to the logged-in user
                AccountNumber = Guid.NewGuid().ToString().Replace("-","").Substring(0, 10), // Generate unique 10-digit account number
                Balance = 0,  // Start with zero balance
                CreatedAt = DateTime.Now  // Record when the account was created
            };

            // BEGINNER NOTE: Save the account to the database (this calls SaveChangesAsync internally)
            await _repo.AddAsync(account);
            return account;
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _repo.GetAllAccountsAsync();
        }

        //public async Task Deposit(int accountId, decimal amount)
        //{
        //    var account = await _repo.GetByIdAsync(accountId);

        //    account.Balance += amount;

        //    _context.Transactions.Add(new Transaction
        //    {
        //        AccountId = accountId,
        //        Amount = amount,
        //        Type = "Deposit",
        //        Date = DateTime.Now
        //    });

        //    await _repo.UpdateAsync(account);
        //}

        /// <summary>
        /// BEGINNER NOTE: This method handles depositing money into an account
        /// Steps: 1) Find the account, 2) Update balance, 3) Save to AccountService DB, 4) Record transaction in TransactionService DB
        /// </summary>
        public async Task Deposit(int accountId, decimal amount, string token)
        {
            // STEP 1: Find the account in the database
            var account = await _repo.GetByIdAsync(accountId);
            
            if (account == null)
                throw new NotFoundException($"Account with ID {accountId} not found");

            // STEP 2: Update the account balance (add deposit amount)
            var oldBalance = account.Balance;
            account.Balance += amount;

            try
            {
                // STEP 3: Save the updated balance to the AccountService database (SmartBankAccountDb)
                await _repo.UpdateAsync(account);

                // STEP 4: Record this transaction in the TransactionService database (MySBTransactionDb)
                // This creates a history log of all deposits/withdrawals
                await _transactionClient.CreateTransaction(new TransactionCreateDto
                {
                    AccountId = accountId,
                    Amount = amount,
                    Type = "Deposit",
                    Description = "Deposit via AccountService"
                }, token);
            }
            catch (Exception ex)
            {
                // BEGINNER NOTE: If transaction recording fails, rollback the balance change to keep data consistent
                // This prevents situations where balance changes but no transaction record exists
                account.Balance = oldBalance;
                await _repo.UpdateAsync(account);
                throw new ApplicationException($"Deposit failed: {ex.Message}. Balance has been restored.", ex);
            }
        }

        //public async Task Withdraw(int accountId, decimal amount)
        //{
        //    var account = await _repo.GetByIdAsync(accountId);

        //    if (account.Balance < amount)
        //        throw new Exception("Insufficient balance");

        //    account.Balance -= amount;

        //    _context.Transactions.Add(new Transaction
        //    {
        //        AccountId = accountId,
        //        Amount = amount,
        //        Type = "Withdraw",
        //        Date = DateTime.Now
        //    });

        //    await _repo.UpdateAsync(account);
        //}

        /// <summary>
        /// BEGINNER NOTE: This method handles withdrawing money from an account
        /// Steps: 1) Find account, 2) Check balance, 3) Update balance, 4) Save to DB, 5) Record transaction
        /// </summary>
        public async Task Withdraw(int accountId, decimal amount, string token)
        {
            // STEP 1: Find the account in the database
            var account = await _repo.GetByIdAsync(accountId);
            
            if (account == null)
                throw new NotFoundException($"Account with ID {accountId} not found");

            // STEP 2: Check if there's enough money to withdraw
            if (account.Balance < amount)
                throw new BadRequestException("Insufficient balance");

            // STEP 3: Update the account balance (subtract withdrawal amount)
            var oldBalance = account.Balance;
            account.Balance -= amount;

            try
            {
                // STEP 4: Save the updated balance to the AccountService database (SmartBankAccountDb)
                await _repo.UpdateAsync(account);

                // STEP 5: Record this transaction in the TransactionService database (MySBTransactionDb)
                await _transactionClient.CreateTransaction(new TransactionCreateDto
                {
                    AccountId = accountId,
                    Amount = amount,
                    Type = "Withdraw",
                    Description = "Withdraw via AccountService"
                }, token);
            }
            catch (Exception ex)
            {
                // BEGINNER NOTE: If transaction recording fails, rollback the balance change
                account.Balance = oldBalance;
                await _repo.UpdateAsync(account);
                throw new ApplicationException($"Withdrawal failed: {ex.Message}. Balance has been restored.", ex);
            }
        }
    }
}
