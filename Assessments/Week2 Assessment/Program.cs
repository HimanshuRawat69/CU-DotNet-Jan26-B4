using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string[] policyHolderNames = new string[5];
        decimal[] annualPremiums = new decimal[5];
        decimal sum = 0;
        decimal average = 0;
        decimal max = -1;
        decimal min = -1;
        for (int i = 0; i < 5; i++)
        {
            
            while (true)
            {
                Console.WriteLine($"Enter Policy Holder {i + 1} Name:");
                policyHolderNames[i] = Console.ReadLine();
                if (policyHolderNames[i] == "")
                {
                    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                }
                else
                {
                    break;
                }
            }
            
            while (true)
            {
                Console.WriteLine($"Enter Policy Holder {i + 1} Annual Premium:");
                annualPremiums[i] = decimal.Parse(Console.ReadLine());
                if (annualPremiums[i] <= 0)
                {
                    Console.WriteLine("Annual Premium must be a positive number. Please enter a valid amount.");
                }
                else
                {
                    break;
                }
            }
            if (annualPremiums[i]>max)
            {
                max = annualPremiums[i];
            }
            if (min==-1 || annualPremiums[i] < min)
            {
                min = annualPremiums[i];
            }
            sum += annualPremiums[i];

        }
        average = sum / 5;


        Console.WriteLine("INSURANCE PREMIUM SUMMARY REPORT");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("{0,-20} {1,15} {2,15}", "Name", "PREMIUM", "CATEGORY");
        Console.WriteLine("------------------------------------------------------------");

        for (int i = 0; i < 5; i++)
        {
            string premiumCategory;

            if (annualPremiums[i] < 10000)
            {
                premiumCategory = "LOW";
            }
            else if (annualPremiums[i] <= 25000)
            {
                premiumCategory = "MEDIUM";
            }
            else
            {
                premiumCategory = "HIGH";
            }

            Console.WriteLine(
                "{0,-20} {1,15:F2} {2,15}",
                policyHolderNames[i].ToUpper(),
                annualPremiums[i],
                premiumCategory
            );
        }

        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("{0,-20} {1,15:F2}", "TOTAL PREMIUM", sum);
        Console.WriteLine("{0,-20} {1,15:F2}", "AVERAGE PREMIUM", average);
        Console.WriteLine("{0,-20} {1,15:F2}", "HIGHEST PREMIUM", max);
        Console.WriteLine("{0,-20} {1,15:F2}", "LOWEST PREMIUM", min);
        Console.WriteLine("------------------------------------------------------------");



    }
}