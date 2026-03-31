namespace SmartBank.TransactionService.DTOs
{
    public class TransactionDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
    }
}
