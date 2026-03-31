namespace SmartBank.AccountService.DTOs
{
    public class AccountWithTransactionsDto
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }

        public List<TransactionDto> Transactions { get; set; }
    }
}
