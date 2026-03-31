namespace SmartBank.Web.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
