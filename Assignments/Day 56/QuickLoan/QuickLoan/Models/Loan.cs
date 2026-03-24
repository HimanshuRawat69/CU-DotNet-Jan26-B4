using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickLoan.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [DisplayName("Borrower Name")]
        public required string BorrowerName { get; set; }

        //[DisplayName("Lender Name")]
        public string? LenderName { get; set; }

        [Range(1, 500000)]
        public double Amount { get; set; }
        public bool IsSettled { get; set; }
    }
}
