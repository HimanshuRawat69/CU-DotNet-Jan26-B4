using System.ComponentModel.Design;

namespace BankTransactionNarrationAnalyzer
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                //Console.WriteLine("Hello, World!");
                //Develop a .NET Console Application that reads a single transaction record,
                //normalizes the narration, searches for key transaction terms,
                //compares it with a standard narration, and displays a formatted summary.

                Console.WriteLine("Enter the Transaction in form TransactionId#AccountHolderName#TransactionNarration :-");
                string input = Console.ReadLine();
                string[] parts = input.Split('#');
                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid input format. Please provide input in the format: TransactionId#AccountHolderName#TransactionNarration");
                    return;
                }
                string transactionId = parts[0];
                string accountHolderName = parts[1];
                string transactionNarration = parts[2];

                // Normalize the narration
                string normalizedNarration = transactionNarration.Trim().ToLower();

                // Define key transaction terms
                string[] keyTerms = { "transfer", "withdrawal", "deposit" };
                List<string> foundTerms = new List<string>();
                foreach (string term in keyTerms)
                {
                    if (normalizedNarration.Contains(term))
                    {
                        foundTerms.Add(term);
                    }
                }


                // Standard narration for comparison
                string standardNarration = "cash deposit successful";
                bool isMatch = (normalizedNarration == standardNarration);

                //Categorization Rules
                string category = "";
                if (foundTerms.Count < 1)
                {
                    category = "NON-FINANCIAL TRANSACTION";
                }
                else if (foundTerms.Count > 0 && isMatch == true)
                {
                    category = "STANDARD TRANSACTION";
                }
                else if (foundTerms.Count > 0 && isMatch == false)
                {
                    category = "CUSTOM TRANSACTION";
                }

                // Display formatted summary
                Console.WriteLine("\n--- Transaction Summary ---");
                Console.WriteLine($"Transaction ID: {transactionId}");
                Console.WriteLine($"Account Holder Name: {accountHolderName}");
                Console.WriteLine($"Narration: {normalizedNarration}");
                Console.WriteLine($"Category:{category}");



            }
        }
    }