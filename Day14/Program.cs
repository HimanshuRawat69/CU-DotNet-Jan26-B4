using System;

class HelloWorld
{
    class Employee
    {
        private int id;

        public void SetId(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public string Name { get; set; }

        private string department;
        public string Department
        {
            get { return department; }
            set
            {
                if (value == "Accounts" || value == "Sales" || value == "IT")
                    department = value;
                else
                    Console.WriteLine("Invalid Department! Allowed: Accounts, Sales, IT");
            }
        }

        private int salary;
        public int Salary
        {
            get { return salary; }
            set
            {
                if (value >= 50000 && value <= 90000)
                    salary = value;
                else
                    Console.WriteLine("Invalid Salary! Must be between 50000 and 90000");
            }
        }

        public void Display()
        {
            Console.WriteLine("Employee Details:");
            Console.WriteLine("Id: " + id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("Salary: " + Salary);
        }
    }

    public static void Main(string[] args)
    {
        Employee emp = new Employee();

        emp.SetId(101);
        emp.Name = "Rawat";
        emp.Department = "IT";
        emp.Salary = 75000;

        emp.Display();

        Console.ReadLine(); 
    }
}
