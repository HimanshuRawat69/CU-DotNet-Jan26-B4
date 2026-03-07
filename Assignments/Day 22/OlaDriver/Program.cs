namespace OlaDriver
{
    class OLADriver
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VehicleNo { get; set; }

        public List<Ride> Rides { get; set; }

        public override string ToString()
        {
            Console.WriteLine($"Driver ID - {Id}, Name - {Name}, Vehicle No - {VehicleNo}");
            foreach (var r in Rides)
            {
                Console.WriteLine(r);
            }
            return "";
        }

    }

    class Ride
    {
        public int RideID { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Fare { get; set; }

        public Ride(int id, string from, string to, decimal amount)
        {
            RideID = id;
            From = from;
            To = to;
            Fare = amount;
        }

        public override string ToString()
        {
            return $"Ride ID - {RideID}, From - {From}, To - {To}, Fare - {Fare}";
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Ride r1 = new Ride(111, "Chandigarh", "Tohana", 3000m);
            Ride r2 = new Ride(112, "Tohana", "Delhi", 3500m);
            Ride r3 = new Ride(113, "Morinda", "Bhatinda", 4000m);
            Ride r4 = new Ride(114, "Bhatinda", "Amritsar", 5000m);
            Ride r5 = new Ride(115, "Chandigarh", "Rewari", 6000m);

            OLADriver d1 = new OLADriver()
            {
                Id = 121,
                Name = "Sneha",
                VehicleNo = "CH234543",
                Rides = new List<Ride> { r1, r2 }
            };
            OLADriver d2 = new OLADriver()
            {
                Id = 122,
                Name = "Prabhnoor",
                VehicleNo = "CH2001201",
                Rides = new List<Ride> { r3, r4 }
            };
            OLADriver d3 = new OLADriver()
            {
                Id = 345,
                Name = "Chirag",
                VehicleNo = "CH9865728",
                Rides = new List<Ride> { r5 }
            };

            Console.WriteLine(d1);
            Console.WriteLine(d2);
            Console.WriteLine(d3);
        }
    }
}


