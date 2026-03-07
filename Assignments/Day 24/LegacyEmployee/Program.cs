using System.Collections;

namespace LegacyEmployee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable employeeTable = new Hashtable();
            employeeTable.Add(101, "Alice");
            employeeTable.Add(102, "Bob");
            employeeTable.Add(103, "Charlie");
            employeeTable.Add(104, "Diana");
            
                if (employeeTable.ContainsKey(105))
                {
                    Console.WriteLine("Id already exists");
                }
                else
                {
                    employeeTable.Add(105, "Edward");
                }
            string employeeName = (string)employeeTable[102];
            Console.WriteLine($"Employee 102's Name: {employeeName} ");
            foreach (DictionaryEntry i in employeeTable)
            {
                Console.WriteLine($"Id: {i.Key}, Name: {i.Value}");
            }
            employeeTable.Remove(103);
            Console.WriteLine($"Total Employees after removing: {employeeTable.Count}");
        }
    }
}
