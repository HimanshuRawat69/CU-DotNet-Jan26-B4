namespace _01Loan
{
    class Loan
    {
        public string LoanNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int TenureYears { get; set; }

        public Loan(string loannumber, string custname, decimal princi, int year)
        {
            LoanNumber = loannumber;
            CustomerName = custname;
            PrincipalAmount = princi;
            TenureYears = year;
        }

        public decimal CalculateEMI()
        {
            Console.WriteLine("Loan class method");
            decimal total = (PrincipalAmount * 10 * TenureYears) / 100;
            Console.WriteLine($"Total EMI: {total}");
            return total;
        }
    }
    class HomeLoan : Loan
    {
        public HomeLoan(string loannumber, string custname, decimal princi, int year) : base(loannumber, custname, princi, year)
        {
            Console.WriteLine("HomeLoan constuctor called");
        }
        public new decimal CalculateEMI()
        {
            Console.WriteLine("HomeLoan class");
            decimal total = 0;
            total = (decimal)(PrincipalAmount) * 0.09m;
            total += 15000;
            return total;
        }
    }

    class CarLoan : Loan
    {
        public CarLoan(string loannumber, string custname, decimal princi, int year) : base(loannumber, custname, princi, year)
        {
            Console.WriteLine("Carloan constructor called");
        }
        public new decimal CalculateEMI()
        {
            Console.WriteLine("CarLoan class");
            decimal total = 0;
            total = (PrincipalAmount) * 0.08m;
            total += (PrincipalAmount) * 0.01m;
            return total;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Loan[] l = new Loan[]
                {
                    new HomeLoan("A1", "Sneha", 800.0m, 6),
                    new HomeLoan("B1","nitesh", 1399.0m, 8),
                    new CarLoan("C1", "amit", 2987.0m, 10),
                    new CarLoan("D1", "Jangel", 4000.0m, 100)
                };

            for (int i = 0; i < l.Length; i++)
            {
                Console.WriteLine();
                l[i].CalculateEMI();
            }
        }
    }
}
