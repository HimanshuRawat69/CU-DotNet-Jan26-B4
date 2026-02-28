//using NUnit.Framework;
public class EmployeeBonus
{
    public decimal BaseSalary { get; set; }
    public int PerformanceRating { get; set; }
    public int YearsOfExperience { get; set; }
    public decimal DepartmentMultiplier { get; set; }
    public double AttendancePercentage { get; set; }

    public decimal NetAnnualBonus {
        get
        {

            decimal percentBonus = 0;

            switch (PerformanceRating)
            {
                case 5:
                    percentBonus = 0.25m;
                    break;

                case 4:
                    percentBonus = 0.18m;
                    break;

                case 3:
                    percentBonus = 0.12m;
                    break;

                case 2:
                    percentBonus = 0.05m;
                    break;

                case 1:
                    percentBonus = 0.00m;
                    break;

                default:
                    throw new InvalidOperationException("Invalid Performance Rating");
            }
            if (BaseSalary <= 0)
            {
                return 0;
            }

            if (AttendancePercentage < 0 || AttendancePercentage > 100)
            {
                throw new InvalidOperationException("Attendance must be between 0 and 100.");
            }
            

            decimal bonus = BaseSalary * percentBonus;

            if (YearsOfExperience > 10)
            {
                bonus += BaseSalary * 0.05m;
            }
            else if (YearsOfExperience > 5)
            {
                bonus += BaseSalary * 0.03m;
            }

            if (AttendancePercentage < 85)
            {
                bonus *= 0.80m;
            }

            bonus *= DepartmentMultiplier;

            decimal maxBonus = BaseSalary * 0.40m;
            if (bonus > maxBonus)
            {
                bonus = maxBonus;
            }

            decimal taxRate = 0;

            if (bonus <= 150000m)
            {
                taxRate = 0.10m;
            }
            else if (bonus <= 300000m)
            {
                taxRate = 0.20m;
            }
            else
            {
                taxRate = 0.30m;
            }

            decimal finalBonus = bonus - (bonus * taxRate);

            return Math.Round(finalBonus, 2);
        }
    }
    
}
internal class Program
{
    private static void Main(string[] args)
    {
        EmployeeBonus emp = new EmployeeBonus()
        {
            BaseSalary = 700000m,
            PerformanceRating = 5,
            YearsOfExperience = 4,
            DepartmentMultiplier = 1.2m,
            AttendancePercentage = 90
        };

        Console.WriteLine("Net Annual Bonus: " + emp.NetAnnualBonus);
    }
}