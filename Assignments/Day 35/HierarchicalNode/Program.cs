namespace HierarchicalNode
{
    public class EmployeeNode
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public List<EmployeeNode> Reports { get; set; } = new List<EmployeeNode>();

        public EmployeeNode(string name, string position)
        {
            Name = name;
            Position = position;
        }
        public void AddReport(EmployeeNode employee)
        {
            Reports.Add(employee);
        }
    }

    public class OrganizationTree
    {
        public EmployeeNode Root { get; private set; }

        public OrganizationTree(EmployeeNode rootEmployee)
        {
            Root = rootEmployee;
        }

        // This method should start the recursion from the Root
        public void DisplayFullHierarchy()
        {
            if (Root == null) return; 
            PrintRecursive(Root, 0);

        }

        private void PrintRecursive(EmployeeNode current, int depth)
        {
            string indent = new string(' ', depth * 4);
            string connector = depth == 0 ? "" : "└── ";

            Console.WriteLine($"{indent}{connector}{current.Name} ({current.Position})");  

            foreach (var report in current.Reports)
            {
                PrintRecursive(report, depth + 1);
            }

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var ceo = new EmployeeNode("Aman", "CEO");
            var cto = new EmployeeNode("Suresh", "CTO");
            var manager = new EmployeeNode("Sonia", "Dev Manager");
            var dev1 = new EmployeeNode("Sara", "Senior Dev");
            var dev2 = new EmployeeNode("Divakar", "Junior Dev");
            var cfo = new EmployeeNode("Rajesh", "CFO");
            var acccOfficer = new EmployeeNode("Rajat", "Account Officer");

            // 2. Build the Tree Structure
            var company = new OrganizationTree(ceo);

            ceo.AddReport(cto);

            cto.AddReport(manager);
            manager.AddReport(dev1);
            manager.AddReport(dev2);

            ceo.AddReport(cfo);
            cfo.AddReport(acccOfficer);

            company.DisplayFullHierarchy();

        }
    }
}
