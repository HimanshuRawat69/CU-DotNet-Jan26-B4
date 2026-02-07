

public class RestrictedDestinationException : Exception
{

    public RestrictedDestinationException(string location)
        
    {
        
    }
}

public class InsecurePackagingException : Exception
{
    public InsecurePackagingException(string message)
    {
    }
}
    public interface ILoggable
    {
        void SaveLog(string message);
    }

    public class LogManager : ILoggable
    {
        private string fileName = "shipment_audit.log";

        public void SaveLog(string message)
        {
            using(StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(message);
            }
        }
    }

    public abstract class Shipment
    {
        public string TrackingId { get; set; }
        public double Weight { get; set; }
        public string Destination { get; set; }
        public bool Fragile { get; set; }
        public bool Reinforced { get; set; }
        List<string> restrictedZones = new List<string>
        {
           "North Pole","Barbados Island"
        };

        public abstract void ProcessShipment();

        public void Check()
        {
            if(Weight <= 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than zero.");
            }

            if(restrictedZones.Contains(Destination))
            {
                throw new RestrictedDestinationException(Destination);
            }

            if(Fragile && !Reinforced)
            {
                throw new InsecurePackagingException("Fragile items must be reinforced.");
            }
        }

    }

    public class ExpressShipment : Shipment
    {
        public override void ProcessShipment()
        {
            Check();
        }

    }

    public class HeavyWeight : Shipment
    {
        public bool heavyLiftPermit { get; set; }

        public override void ProcessShipment()
        {
            Check();
            if(Weight > 1000 && !heavyLiftPermit)
            {
                throw new Exception("Heavy lift permit required for shipment: " +TrackingId);
            }
            Console.WriteLine("Shipment processed: " +TrackingId);
    }

    }
    internal class Program
    {
        private static void Main(string[] args)
        {

            LogManager lm=new LogManager();
            List<Shipment> l = new List<Shipment>()
            {
                new ExpressShipment
                {
                    TrackingId = "1245",
                    Weight = 50,
                    Destination = "Julana",
                    Fragile = false,
                    Reinforced = false
                },

                new ExpressShipment
                {
                    TrackingId = "1246",
                    Weight = 10000,
                    Destination = "North Pole",
                    Fragile = false,
                    Reinforced = false
                },
                new HeavyWeight
                {
                TrackingId = "1247",
                Weight = 1500,
                Destination = "Rohtak",
                Fragile = false,
                Reinforced = false,
                heavyLiftPermit = false
                },
                new HeavyWeight
                {
                TrackingId = "1248",
                Weight = 1500,
                Destination = "Rohtak",
                Fragile = true,
                Reinforced = false,
                heavyLiftPermit = false
                }


            };


            foreach (Shipment shipment in l)
            {
                try
                {
                    shipment.ProcessShipment();
                    lm.SaveLog("Shipment Processsed with no exception");
                }
                catch (RestrictedDestinationException ex)
                {
                    lm.SaveLog("SECURITY ALERT: " + ex.Message+"for ID:"+ shipment.TrackingId);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    lm.SaveLog("DATA ENTRY ERROR: " + ex.Message + "for ID:" + shipment.TrackingId);
                }
                catch(Exception ex)
                {
                    lm.SaveLog("General Exception:"+ex.Message + "for ID:" + shipment.TrackingId);
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for ID: {shipment.TrackingId}.");
                }
            }
        }


    }
