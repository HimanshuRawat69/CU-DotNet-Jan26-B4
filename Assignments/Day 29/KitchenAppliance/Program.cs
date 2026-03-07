namespace KitchenAppliance
{
        interface ITimer
        {
            public void SetTimer(int mins);
        }

        interface ISmart
        {
            public void ConnectWifi();
        }

        abstract class KitchenElecAppl
        {
            public int PowerWatts { get; set; }
            public string ModelName { get; set; }
            public KitchenElecAppl(int powerWatts, string modelName)
            {
                PowerWatts = powerWatts;
                ModelName = modelName;
            }
            public abstract void Cook();

            public virtual void Preheat() { }
        }

        class Microwave : KitchenElecAppl, ITimer
        {
            public Microwave(int powerWatts, string modelName) : base(powerWatts, modelName)
            {
            }

            public void SetTimer(int mins)
            {
                Console.WriteLine($"Microwave timer set for {mins} minutes.");
            }
            public override void Cook()
            {
                Console.WriteLine("Microwave is heating food.");
            }

        }

        class ElectricOven : KitchenElecAppl, ITimer, ISmart
        {
            public ElectricOven(int powerWatts, string modelName) : base(powerWatts, modelName)
            {
            }

            public void SetTimer(int mins)
            {
                Console.WriteLine($"Oven timer set for {mins} minutes.");
            }
            public override void Preheat()
            {
                Console.WriteLine("Oven is preheating.");
            }
            public void ConnectWifi()
            {
                Console.WriteLine("Electric Oven connected to WiFi.");
            }
            public override void Cook()
            {
                Preheat();
                Console.WriteLine("Oven is cooking food.");
            }
        }

        class AirFryer : KitchenElecAppl
        {
            public AirFryer(int powerWatts, string modelName) : base(powerWatts, modelName)
            {
            }

            public override void Cook()
            {
                Console.WriteLine("Air frying is cooking");
            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                List<KitchenElecAppl> appliances = new List<KitchenElecAppl>
        {
            new Microwave(900,"QuickWave900"),
            new ElectricOven(2400,"AeroCook Pro"),
            new AirFryer(1500, "Philips Airfryer")
        };

                foreach (var appliance in appliances)
                {
                    Console.WriteLine($"{appliance.ModelName}:");
                    appliance.Cook();
                    if (appliance is Microwave)
                    {
                        var obj = (Microwave)appliance;
                        obj.SetTimer(20);
                    }
                    if (appliance is ElectricOven)
                    {
                        ElectricOven oven = (ElectricOven)appliance;
                        oven.SetTimer(30);
                        oven.ConnectWifi();
                    }

                }
            }
        }
    }
