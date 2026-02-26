using System.IO;

namespace LoanPortfolio
{
    public class Loan
    {
        public string ClientName { get; set; }
        public double Principal { get; set; }
        public double InterestRate { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"../../../loans.csv";

            using StreamWriter wr = new StreamWriter(fileName, true);
            //wr.WriteLine("ClientName, Principal,InterestRate");
            int input;
            do
            {
                Console.WriteLine("enter 1 to add, 0 to stop");
                input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    break;
                }
                Console.Write("Enter client name");
                string name = Console.ReadLine();

                Console.Write("Enter principal amount");
                double principal = double.Parse(Console.ReadLine());

                Console.Write("Enter interest rate (%): ");
                double rate = double.Parse(Console.ReadLine());

                wr.WriteLine($"{name},{principal},{rate}");
            } while (true);
            wr.Close();

            using StreamReader sm = new StreamReader(fileName);
            while (true)
            {
                string read = sm.ReadLine();
                if (read == null) break;

                try
                {
                    string[] arr = read.Split(',');

                    Loan loan1 = new Loan
                    {
                        ClientName = arr[0],
                        Principal = double.Parse(arr[1]),
                        InterestRate = double.Parse(arr[2])
                    };

                    string risk;
                    if (loan1.InterestRate > 10)
                        risk = "High risk";
                    else if (loan1.Principal >= 5000)
                        risk = "Medium risk";
                    else
                        risk = "Low risk";

                    double amount = loan1.Principal * loan1.InterestRate / 100;
                    Console.WriteLine($"{loan1.ClientName} | Interest: ${amount} | {risk}");
                }
                catch
                {
                    Console.WriteLine("Invalid data in file.");
                }
            }






        }
    }
}
