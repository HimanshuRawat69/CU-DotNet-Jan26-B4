namespace SaleProcessor
{
    internal class Program
    {
            static void InputDailyRevenue(decimal[] revenuePerDay)
            {
                int index = 0;
                while (index < 7)
                {
                    Console.Write($"Enter revenue for Day {index + 1}: ");
                    revenuePerDay[index] = decimal.Parse(Console.ReadLine());

                    if (revenuePerDay[index] < 0)
                    {
                        Console.WriteLine("Invalid input! Revenue cannot be negative.");
                        continue;
                    }
                    index++;
                }
            }

            static decimal GetTotalRevenue(decimal[] revenuePerDay)
            {
                decimal total = 0;
                foreach (decimal value in revenuePerDay)
                {
                    total += value;
                }
                return total;
            }

            static decimal GetAverageRevenue(decimal totalRevenue, int days)
            {
                return totalRevenue / days;
            }

            static decimal GetMaximumRevenue(decimal[] revenuePerDay, out int dayIndex)
            {
                decimal maxValue = revenuePerDay[0];
                dayIndex = 0;

                for (int i = 1; i < revenuePerDay.Length; i++)
                {
                    if (revenuePerDay[i] > maxValue)
                    {
                        maxValue = revenuePerDay[i];
                        dayIndex = i;
                    }
                }
                return maxValue;
            }

            static decimal GetMinimumRevenue(decimal[] revenuePerDay, out int dayIndex)
            {
                decimal minValue = revenuePerDay[0];
                dayIndex = 0;

                for (int i = 1; i < revenuePerDay.Length; i++)
                {
                    if (revenuePerDay[i] < minValue)
                    {
                        minValue = revenuePerDay[i];
                        dayIndex = i;
                    }
                }
                return minValue;
            }

            static decimal ComputeDiscount(decimal totalRevenue)
            {
                decimal rate = (totalRevenue >= 50000) ? 10m : 5m;
                return (rate / 100m) * totalRevenue;
            }

            static decimal ComputeDiscount(decimal totalRevenue, bool festivalPeriod)
            {
                decimal rate = (totalRevenue >= 50000) ? 15m : 10m;
                return (rate / 100m) * totalRevenue;
            }

            static decimal ComputeTax(decimal netAmount)
            {
                decimal taxPercent = 18m;
                return (taxPercent / 100m) * netAmount;
            }

            static decimal GetFinalPayable(decimal totalRevenue, decimal discount, decimal tax)
            {
                return totalRevenue - discount - tax;
            }

            static void AssignRevenueCategory(decimal[] revenuePerDay, string[] categoryPerDay)
            {
                for (int i = 0; i < revenuePerDay.Length; i++)
                {
                    if (revenuePerDay[i] < 5000)
                        categoryPerDay[i] = "Low";
                    else if (revenuePerDay[i] > 15000)
                        categoryPerDay[i] = "High";
                    else
                        categoryPerDay[i] = "Medium";
                }
            }

            static void Main(string[] args)
            {
                Console.WriteLine("Weekly Revenue Entry");
                decimal[] dailyRevenue = new decimal[7];

                InputDailyRevenue(dailyRevenue);

                decimal totalRevenue = GetTotalRevenue(dailyRevenue);
                decimal averageRevenue = GetAverageRevenue(totalRevenue, dailyRevenue.Length);

                int bestDay, worstDay;
                decimal highestRevenue = GetMaximumRevenue(dailyRevenue, out bestDay);
                decimal lowestRevenue = GetMinimumRevenue(dailyRevenue, out worstDay);

                bool isFestivalPeriod = true;
                decimal discountValue = isFestivalPeriod
                    ? ComputeDiscount(totalRevenue, isFestivalPeriod)
                    : ComputeDiscount(totalRevenue);

                decimal taxValue = ComputeTax(totalRevenue - discountValue);
                decimal finalAmount = GetFinalPayable(totalRevenue, discountValue, taxValue);

                string[] revenueCategory = new string[7];
                AssignRevenueCategory(dailyRevenue, revenueCategory);

                Console.WriteLine("\n Weekly Revenue Report");
                Console.WriteLine($"Total Revenue          : {totalRevenue}");
                Console.WriteLine($"Average Daily Revenue  : {averageRevenue}");

                Console.WriteLine($"\nHighest Revenue        : {highestRevenue:F2} (Day {bestDay + 1})");
                Console.WriteLine($"Lowest Revenue         : {lowestRevenue:F2} (Day {worstDay + 1})");

                Console.WriteLine($"\nDiscount Amount        : {discountValue}");
                Console.WriteLine($"Tax Amount             : {taxValue}");
                Console.WriteLine($"Final Payable Amount   : {finalAmount}");

                Console.WriteLine("\nDay-wise Revenue Category");
                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine($"Day {i + 1}: {revenueCategory[i]}");
                }
            }
        }
    }

