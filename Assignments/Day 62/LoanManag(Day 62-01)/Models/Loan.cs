using System.ComponentModel.DataAnnotations;

namespace LoanManagementWebAPI.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        
        [Required]
        public string BorrowerName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int LoanTermMonths { get; set; }

        [Required]
        public bool isApproved { get; set; }
    }
}
