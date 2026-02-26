namespace _02Inheritance
{
    class Employee1
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public int ExperienceInYears { get; set; }
        public Employee1(int employeeId, string employeeName, decimal basicSalary, int experienceInYears)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            BasicSalary = basicSalary;
            ExperienceInYears = experienceInYears;
        }
        public decimal CalculateAnnualSalary()
        { 
            return BasicSalary * 12;
        }
        public void DisplayDetails()
        {
            Console.WriteLine($"Employee Id: {EmployeeId}");
            Console.WriteLine($"Employee Name: {EmployeeName}");
            Console.WriteLine($"Employee's basic salary: {BasicSalary}");
            Console.WriteLine($"Employee's experience in Years: {ExperienceInYears}");
            Console.WriteLine($"Annual salary: {CalculateAnnualSalary()}");
        }
    }
    
    class PermanentEmployee : Employee1
    {
        public PermanentEmployee(int id, string name, decimal basicSalary, int experience)
        : base(id, name, basicSalary, experience)
        {
            Console.WriteLine("i am default permanentemployee constructor");
        }
        public new decimal CalculateAnnualSalary()
        {
            decimal hra = BasicSalary * 0.20m;
            decimal specialAllowance = BasicSalary * 0.10m;
            decimal bonus = 0;
            if(ExperienceInYears >= 5)
            {
                bonus = 50000;
            }

            return (BasicSalary * 12) + (hra * 12) + (specialAllowance * 12) + bonus;
        }
    }
    
    class ContractEmployee : Employee1
    {
        public int ContractDurationInMonths { get; set; }
        public ContractEmployee(int id, string name, decimal basicSalary, int experience, int duration)
        : base(id, name, basicSalary, experience)
        {
            ContractDurationInMonths = duration;
            Console.WriteLine("i am default contractemployee constructor");
        }
        public new decimal CalculateAnnualSalary()
        {
            decimal compBonus = 0;
            if (ContractDurationInMonths > 12)
            {
                compBonus = 30000m;
            }
            return (BasicSalary * 12) + compBonus;
        }
    }
    
    class InternEmployee : Employee1
    {
        public InternEmployee(int id, string name, decimal basicSalary, int experience)
        : base(id, name, basicSalary, 0)
        {
            Console.WriteLine("i am default internemployee constructor");
        }
        public new decimal CalculateAnnualEmployee()
        {
            return BasicSalary * 12;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee1 e1 = new PermanentEmployee(101, "A", 40000, 6);
            PermanentEmployee e2 = new PermanentEmployee(102, "B", 40000, 6);

            Employee1 e3 = new ContractEmployee(103, "Ce", 30000, 2, 14);
            ContractEmployee e4 = new ContractEmployee(104, "D", 30000, 2, 14);

            Employee1 e5 = new InternEmployee(105, "E", 15000, 0);
            InternEmployee e6 = new InternEmployee(106, "F", 15000, 0);

            Console.WriteLine("Base class refernce:");
            Console.WriteLine(e1.CalculateAnnualSalary());
            Console.WriteLine(e3.CalculateAnnualSalary());
            Console.WriteLine(e5.CalculateAnnualSalary());

            Console.WriteLine("Derived class refernce:");
            Console.WriteLine(e2.CalculateAnnualSalary()); 
            Console.WriteLine(e4.CalculateAnnualSalary()); 
            Console.WriteLine(e6.CalculateAnnualSalary());
        }
    }
}
