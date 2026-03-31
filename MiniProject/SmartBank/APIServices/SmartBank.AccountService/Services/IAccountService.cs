using SmartBank.AccountService.Models;

namespace SmartBank.AccountService.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(string userId);
        Task<Account> GetAccount(int id);

        Task<List<Account>> GetAllAccounts();
        Task Deposit(int accountId, decimal amount, string token);
        Task Withdraw(int accountId, decimal amount, string token);
    }
}
