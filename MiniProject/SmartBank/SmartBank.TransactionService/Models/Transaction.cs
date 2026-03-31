namespace SmartBank.TransactionService.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }

        public string Type { get; set; } = string.Empty;   // Credit / Debit
        public string UserId { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
