using SmartBank.Data;
using SmartBank.Models;
using System;

namespace SmartBank.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SmartBankContext _context;

        public AccountRepository(SmartBankContext context)
        {
            _context = context;
        }
        

        public Account Create(Account account)
        {
            _context.Account.Add(account);
            _context.SaveChanges();
            return account;
        }

        public List<Account> GetAll()
        {
            return _context.Account.ToList();
        }

        public Account GetById(int id)
        {
            return _context.Account.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Account account)
        {
            _context.Account.Update(account);
            _context.SaveChanges();
        }
    }
}
