using SmartBank.Models;

namespace SmartBank.Repositories
{
    public interface IAccountRepository
    {
        Account Create(Account account);

        List<Account> GetAll();

        Account GetById(int id);

        void Update(Account account);
    }
}
