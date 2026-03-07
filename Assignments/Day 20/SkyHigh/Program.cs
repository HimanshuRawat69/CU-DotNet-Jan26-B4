namespace SkyHigh
{
    class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DepartureTime { get; set; }

        public int CompareTo(Flight? other)
        {
            //throw new NotImplementedException();
            return this.Price.CompareTo(other?.Price);
        }
        public override string ToString()
        {
            return $"{FlightNumber} | {Price} | {Duration} | {DepartureTime:d}";
        }
    }
    class DurationComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            return x.Duration.CompareTo(y?.Duration);
            //throw new NotImplementedException();
        }
    }

    class DepartureComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y)
        {
            return x.DepartureTime.CompareTo(y?.DepartureTime);
            //throw new NotImplementedException();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Flight> flights = new List<Flight>()
            {
                new Flight()
                {
                    FlightNumber = "SH101",
                    Price = 4500,
                    Duration = new TimeSpan(2,30,0),
                    DepartureTime = new DateTime(2026, 01, 28, 2, 30,0)
                },
                new Flight()
                {
                    FlightNumber = "SH102",
                    Price = 8000,
                    Duration = new TimeSpan(1, 45, 0),
                    DepartureTime = new DateTime(2026, 1, 28, 5, 15, 0)
                },
                new Flight()
                {
                    FlightNumber = "SH103",
                    Price = 10000,
                    Duration = new TimeSpan(3, 10, 0),
                    DepartureTime = new DateTime(2026, 1, 28, 1, 45, 0)
                }
            };
            flights.Sort();
            Console.WriteLine("Economy View");
            foreach(var flight in flights)
            {
                Console.WriteLine(flight);
            }
            Console.WriteLine();
            flights.Sort(new DurationComparer());
            Console.WriteLine("Business Runner View");
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
            Console.WriteLine();
            flights.Sort(new DepartureComparer());
            Console.WriteLine("Early Bird View");
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}
