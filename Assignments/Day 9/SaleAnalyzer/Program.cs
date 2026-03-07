namespace SaleAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {


            decimal[] weekSale = new decimal[7];
            for (int i = 0; i < weekSale.Length; i++)
            {
                Console.Write($"Enter Sales Day {i + 1}: ");
                weekSale[i] = Convert.ToDecimal(Console.ReadLine());

                if (weekSale[i] < 0)
                {
                    Console.WriteLine("Sales can't be negative. Try Again.");
                    i--;
                }

            }
            decimal totalSales = 0;
            for (int i = 0; i < weekSale.Length; i++)
            {
                totalSales += weekSale[i];
            }
            decimal averageSales = totalSales / weekSale.Length;

            decimal lowestsale = weekSale.Min();
            int positionLowest = Array.IndexOf(weekSale, lowestsale) + 1;
            decimal highestsale = weekSale.Max();
            int positionHighest = Array.IndexOf(weekSale, highestsale) + 1;

            string[] category = new string[7];
            for (int i = 0; i < weekSale.Length; i++)
            {
                if (weekSale[i] < 5000)
                {
                    category[i] = "Low";
                }
                else if (weekSale[i] >= 5000 && weekSale[i] < 15000)
                {
                    category[i] = "Medium";
                }
                else
                {
                    category[i] = "High";
                }
            }


            Console.WriteLine("\nWeekly Sales Report:");
            Console.WriteLine($"\nTotal Sales for the week: {totalSales}");
            Console.WriteLine($"Average Daily Sale:{averageSales} ");
            Console.WriteLine($"\nLowest Sale: {lowestsale} on Day {positionLowest}");
            Console.WriteLine($"Highest Sale: {highestsale} on Day {positionHighest}");

            int count = 0;
            for (int i = 0; i < weekSale.Length; i++)
            {
                if (weekSale[i] > averageSales)
                {
                    count += 1;
                }
            }
            Console.WriteLine($"Days with sales greater than weekly average:{count}");


            Console.WriteLine("\nDay-wise Sales Category:");
            for (int i = 0; i < category.Length; i++)
            {
                Console.WriteLine($"Day {i + 1}: {category[i]}");
            }

        }
    }
}