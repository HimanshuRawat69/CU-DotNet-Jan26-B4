using Microsoft.EntityFrameworkCore;
using SmartBank.DTO;
using SmartBank.Exceptions;
using SmartBank.Helpers;
using SmartBank.Models;
using SmartBank.Repositories;

namespace SmartBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Account CreateAccount(CreateAccountDto dto, string email)
        {
            var account = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Balance = dto.InitialDeposit,
                Email = email
            };

            _repository.Create(account);

            return account;
        }

        public List<AccountDto> GetAll()
        {
            var accounts = _repository.GetAll();

            List<AccountDto> result = new List<AccountDto>();

            foreach (var account in accounts)
            {
                AccountDto dto = new AccountDto();

                dto.Id = account.Id;
                dto.AccountNumber = account.AccountNumber;
                dto.Name = account.Name;
                dto.Balance = account.Balance;

                result.Add(dto);
            }

            return result;
        }

        public AccountDto GetById(int id)
        {
            var account = _repository.GetById(id);

            if (account == null)
                throw new NotFoundException("Account not found");

            AccountDto dto = new AccountDto();

            dto.Id = account.Id;
            dto.AccountNumber = account.AccountNumber;
            dto.Name = account.Name;
            dto.Balance = account.Balance;

            return dto;
        }

        public void Deposit(TransactionDto dto)
        {
            var account = _repository.GetById(dto.AccountId);

            if (account == null)
                throw new NotFoundException("Account not found");

            if (dto.Amount <= 0)
                throw new BadRequestException("Amount must be greater than 0");

            account.Balance += dto.Amount;

            _repository.Update(account);
        }

        public void Withdraw(TransactionDto dto)
        {
            var account = _repository.GetById(dto.AccountId);

            if (account == null)
                throw new NotFoundException("Account not found");

            if (account.Balance - dto.Amount < 1000)
                throw new BadRequestException("Minimum balance ₹1000 required");

            account.Balance -= dto.Amount;

            _repository.Update(account);
        }
    }
}
