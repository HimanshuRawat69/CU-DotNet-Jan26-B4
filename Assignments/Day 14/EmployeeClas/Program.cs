namespace EmployeeClas
{
        class Employee
        {
            int id;
            public void SetId(int id)
            {
                this.id = id;
            }
            public void GetId()
            {
                Console.WriteLine($"{id}");
            }
            public string Name { get; set; }

            private string department;

            public string Department
            {
                get { return department; }
                set
                {
                    if (value == "Accounts" || value == "IT" || value == "Sales")
                        department = value;
                }
            }
            private int salary;

            public int Salary
            {
                get { return salary; }
                set
                {
                    if (value > 50000 && value < 90000)
                        salary = value;
                }
            }

        }
        internal class Demo01
        {
            static void Main(string[] args)
            {
                Employee employee1 = new Employee();
                employee1.SetId(123444);
                employee1.GetId();

                employee1.Name = "Sneha";
                Console.WriteLine(employee1.Name);

                employee1.Department = "Accounts";
                Console.WriteLine(employee1.Department);

                employee1.Salary = 60000;
                Console.WriteLine(employee1.Salary);
            }
        }
    }


