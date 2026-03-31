namespace SmartBank.AccountService.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Deposit / Withdraw
        public DateTime Date { get; set; }
    }
}
