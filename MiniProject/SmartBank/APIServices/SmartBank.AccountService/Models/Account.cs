namespace SmartBank.AccountService.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // from AuthService
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
