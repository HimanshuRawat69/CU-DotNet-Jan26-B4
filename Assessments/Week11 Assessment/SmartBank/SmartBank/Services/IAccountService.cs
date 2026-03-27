using SmartBank.DTO;
using SmartBank.Models;

namespace SmartBank.Services
{
    public interface IAccountService
    {
        Account CreateAccount(CreateAccountDto dto, string email);

        List<AccountDto> GetAll();

        AccountDto GetById(int id);

        void Deposit(TransactionDto dto);

        void Withdraw(TransactionDto dto);
    }
}
