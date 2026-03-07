namespace Problem1
{
    internal class Program
    {
        abstract class Vehicle
        {
            public string ModelName { get; set; }
            protected Vehicle(string name)
            {
                ModelName = name; 
            }
            public abstract void Move();
            public virtual string GetFuelStatus()
            {
                return $"Fuel Level is stable. ";
            }
        }

        class ElectricCar : Vehicle
        {
            public ElectricCar(string name) : base(name)
            {
            }

            public override void Move()
            {
                Console.WriteLine($"{ModelName} is gliding silently on battery power");
            }
            public override string GetFuelStatus()
            {
                return $"{ModelName} battery is at 80%";
            }
        }

        class HeavyTruck: Vehicle
        {
            public HeavyTruck(string name) : base(name)
            {
            }
            public override void Move()
            {
                Console.WriteLine($"{ModelName} is hauling cargo with high-torque diesel power");
            }
            
        }

        class CargoPlane : Vehicle
        {
            public CargoPlane(string name) : base(name)
            {
            }
            public override void Move()
            {
                Console.WriteLine($"{ModelName} is ascending to 30,000 feet");
            }
            public override string GetFuelStatus()
            {
                return base.GetFuelStatus() + $" Checking jet fuel reserves. ";
            }
        }

        static void Main(string[] args)
        {
            
            Vehicle[] v1 = new Vehicle[]
            {
                new ElectricCar("Thar"),
                new HeavyTruck("Maruti"),
                new CargoPlane("Tesla")
            };
            
            foreach (var v in v1) 
            {
                v.Move();
                Console.WriteLine(v.GetFuelStatus());
            }
        }
    }
}
