namespace FreightTracking
{
    class RestrictedDestinationException : Exception
    {
        public string DeniedLocation { get; set; }
        public RestrictedDestinationException(string location) : base($"Shipment destination {location} is denied/restricted")
        {
            DeniedLocation = location;
        }
    }

    class InsecurePackagingException : Exception
    {
        public InsecurePackagingException(string message) : base(message) { }
    }
    abstract class Shipment
    {
        public string TrackingId { get; set; }
        public double Weight { get; set; }
        public string Destination { get; set; }

        public bool IsFragile { get; set; }
        public bool IsReinforced { get; set; }

        public void Validate()
        {
            if (Weight <= 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than 0");
            }


            if (IsFragile == true && IsReinforced == false)
            {
                throw new InsecurePackagingException("Fragile package must be reinforced");
            }

            if (Destination == "North Pole" || Destination == "Unknown Island")
            {
                throw new RestrictedDestinationException(Destination);
            }
        }

        public abstract void ProcessShipment();
    }

    class ExpressShipment : Shipment
    {
        public override void ProcessShipment()
        {
            Console.WriteLine($"{TrackingId} - Express Shipment processed quickly.");
        }
    }

    class HeavyFreight : Shipment
    {
        public override void ProcessShipment()
        {
            if (Weight > 1000)
            {
                Console.WriteLine($"{TrackingId} - A special Heavy Lift permit required.");
            }
            Console.WriteLine($"{TrackingId} - Heavy Freight Shipment processed.");
        }
    }

    interface ILoggable
    {
        void SaveLog(string message);
    }
    class LogManager : ILoggable
    {

        string fileName = @"../../../shipment_audit.log";
        public void SaveLog(string message)
        {
            using StreamWriter sw = new StreamWriter(fileName, true);
            sw.WriteLine(message);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            LogManager logMan = new LogManager();
            List<Shipment> shipments = new List<Shipment>
            {
                new ExpressShipment
                {
                    TrackingId = "T1801",
                    Weight = 500,
                    Destination = "Delhi",
                    IsFragile = true,
                    IsReinforced = true
                },
                new HeavyFreight
                {
                    TrackingId = "T1802",
                    Weight = 1500,
                    Destination = "Chandigarh",
                    IsFragile = false,
                    IsReinforced = false
                },
                new ExpressShipment
                {
                    TrackingId = "T1803",
                    Weight = -50,
                    Destination = "Mumbai",
                    IsFragile = false,
                    IsReinforced = false
                },
                new HeavyFreight
                {
                    TrackingId = "T1804",
                    Weight = 800,
                    Destination = "North Pole",
                    IsFragile = false,
                    IsReinforced = false
                },
                new ExpressShipment
                {
                    TrackingId = "T1805",
                    Weight = 120,
                    Destination = "Pune",
                    IsFragile = true,
                    IsReinforced = false
                },
            };

            foreach (Shipment ship in shipments)
            {
                try
                {
                    ship.Validate();
                    ship.ProcessShipment();
                    logMan.SaveLog($"SUCCESS: your package {ship.TrackingId} has been processed");
                }

                catch (RestrictedDestinationException ex)
                {
                    logMan.SaveLog($"SECURITY ALERT: Shipment blocked for {ex.DeniedLocation}. Shipment Denied");
                }

                catch (InsecurePackagingException ex)
                {
                    logMan.SaveLog($"Packaging Error: {ex.Message}");
                }

                catch (ArgumentOutOfRangeException ex)
                {
                    logMan.SaveLog($"DATA ENTRY ERROR {ex.Message}");
                }

                catch (Exception ex)
                {
                    logMan.SaveLog($"GENERAL ERROR {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for Id: {ship.TrackingId}");
                }
            }
        }
    }
}
