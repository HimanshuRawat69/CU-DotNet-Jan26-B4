namespace Week2Assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] policyHolderNames = new string[5];
            decimal[] annualPremiums = new decimal[5];



            //Input
            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                Console.Write($"Enter the name of policy holder {i + 1}: ");
                policyHolderNames[i] = Console.ReadLine();
                Console.Write($"Enter the annual premium for {policyHolderNames[i]}: ");
                annualPremiums[i] = Convert.ToDecimal(Console.ReadLine());

                if (policyHolderNames[i] == "")
                {
                    Console.WriteLine("Policy holder name cannot be empty. Try Again.");
                    i--;
                }
                if (annualPremiums[i] < 0)
                {
                    Console.WriteLine("Annual premium cannot be negative. Try Again.");
                    i--;
                }
            }

            //2.Processing
            decimal totalPremium = 0;
            decimal minPremium = annualPremiums[0];
            decimal maxPremium = annualPremiums[0];

            for (int i = 0; i < 5; i++)
            {
                totalPremium += annualPremiums[i];

                if (annualPremiums[i] < minPremium)
                    minPremium = annualPremiums[i];

                if (annualPremiums[i] > maxPremium)
                    maxPremium = annualPremiums[i];
            }

            decimal averagePremium = totalPremium / 5;


            string category = "";

            // Output Report
            Console.WriteLine("\nInsurance Premium Summary:");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("\n Name \t Premium \t Category");
            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                if (annualPremiums[i] < 10000)
                {
                    category = "Low";
                }
                else if (annualPremiums[i] >= 10000 && annualPremiums[i] < 25000)
                {
                    category = "Medium";
                }
                else
                {
                    category = "Premium";
                }
                Console.WriteLine($"{policyHolderNames[i].ToUpper()} \t {annualPremiums[i]:F2} \t {category}");
            }
            Console.WriteLine($"\nTotal Premium: {totalPremium:F2}");
            Console.WriteLine($"Average Premium: {averagePremium:F2}");
            Console.WriteLine($"Highest Premium: {maxPremium:F2}");
            Console.WriteLine($"Lowest Premium: {minPremium:F2}");
        }
    }
}
