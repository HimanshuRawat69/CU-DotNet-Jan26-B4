namespace SmartBank.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string? AccountNumber { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
