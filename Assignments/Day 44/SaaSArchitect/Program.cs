using System.Xml.Linq;
namespace SaaSArchitect
{ 
        abstract class Subscriber : IComparable<Subscriber>
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public DateTime JoinDate { get; set; }

            public Subscriber(Guid iD, string name, DateTime joinDate)
            {
                ID = iD;
                Name = name;
                JoinDate = joinDate;
            }

            public abstract decimal CalculateMonthlyBill();

            public override bool Equals(object? obj)
            {
                if (obj is Subscriber other)
                    return ID.Equals(other.ID);

                return false;
            }

            public override int GetHashCode()
            {
                return ID.GetHashCode();
            }



            public int CompareTo(Subscriber other)
            {
                int dateCompare = JoinDate.CompareTo(other.JoinDate);
                if (dateCompare != 0)
                    return dateCompare;

                return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
            }
        }

        class BussinessSubscriber : Subscriber
        {
            public decimal FixedRate { get; set; }
            public decimal TaxRate { get; set; }

            public BussinessSubscriber(Guid id, string name, DateTime date, decimal frate, decimal trate) : base(id, name, date)
            {
                FixedRate = frate;
                TaxRate = trate;
            }
            public override decimal CalculateMonthlyBill()
            {
                return FixedRate * (1 + TaxRate);
            }
        }

        class ConsumerSubscriber : Subscriber
        {
            public decimal DataUsageGB { get; set; }
            public decimal PricePerGB { get; set; }

            public ConsumerSubscriber(Guid id, string name, DateTime date, decimal usage, decimal perGB) : base(id, name, date)
            {
                DataUsageGB = usage;
                PricePerGB = perGB;
            }

            public override decimal CalculateMonthlyBill()
            {
                return DataUsageGB * PricePerGB;
            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                Dictionary<string, Subscriber> sub = new()
                {
                    ["user1@abc.com"] = new BussinessSubscriber(
                    Guid.NewGuid(), "Rohan Roy", new DateTime(2023, 1, 10), 1000m, 0.18m),

                    ["user2@abc.com"] = new BussinessSubscriber(
                    Guid.NewGuid(), "Alpha Omega", new DateTime(2022, 12, 5), 800m, 0.15m),

                    ["consumer@gmail.com"] = new ConsumerSubscriber(
                    Guid.NewGuid(), "Rahul", new DateTime(2024, 3, 1), 50m, 2.5m),

                    ["Hello@gmail.com"] = new ConsumerSubscriber(
                    Guid.NewGuid(), "Sham", new DateTime(2024, 1, 20), 30m, 3m),

                    ["lmao@gmail.com"] = new ConsumerSubscriber(
                    Guid.NewGuid(), "Lakhan", new DateTime(2023, 11, 15), 70m, 2m)
                };

                var sortedForReport = sub.OrderByDescending(kvp => kvp.Value.CalculateMonthlyBill()).Select(kvp => kvp.Value).ToList();

                
                Console.WriteLine("NAME\t\tTYPE\t\tJOIN DATE\tBILL");
                Console.WriteLine("----------------------------------------------------");

                foreach (var s in sortedForReport)
                {
                    string type = s is BussinessSubscriber ? "Business" : "Consumer";

                    Console.WriteLine(
                        $"{s.Name,-15}{type,-15}{s.JoinDate:yyyy-MM-dd}\t{s.CalculateMonthlyBill()}");
                }

            }
        }
    }

