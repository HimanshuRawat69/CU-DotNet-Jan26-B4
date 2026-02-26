using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Problem2
{
    abstract class UtilityBill
    {
        public int ConsumerId { get; set; }
        public string ConsumerName { get; set; }
        public decimal UnitsConsumed { get; set; }
        public decimal RatePerUnit { get; set; }
        protected UtilityBill(int id, string name, decimal unit, decimal rate)
        {
            ConsumerId = id;
            ConsumerName = name;
            UnitsConsumed = unit;
            RatePerUnit = rate;
        }
        public abstract decimal CalculateBillAmount();
        public virtual decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.05m;
        }
        public void PrintBill()
        {
            //decimal billAmount = CalculateBillAmount();
            //decimal tax = CalculateTax(billAmount);
            decimal total = CalculateBillAmount() + CalculateTax(CalculateBillAmount());
            Console.WriteLine($"Consumer ID: {ConsumerId}");
            Console.WriteLine($"Consumer Name: {ConsumerName}");
            Console.WriteLine($"Total Units: {UnitsConsumed}");
            //Console.WriteLine($"Bill Amount: {billAmount}");
            //Console.WriteLine($"Tax: {tax}");
            Console.WriteLine($"Final Payable amount: {total}");
        }
    }
    class ElectricityBill : UtilityBill
    {
        public ElectricityBill(int id, string name, decimal unit, decimal rate) : base(id, name, unit, rate)
        {
            //Console.WriteLine(" i am electricty bill ctor");
        }
        public override decimal CalculateBillAmount()
        {
            decimal amount = UnitsConsumed * RatePerUnit;
            if (UnitsConsumed > 300)
            {
                amount += amount * 0.10m;
            }
            return amount;
        }
    }
    class WaterBill : UtilityBill
    {
        public WaterBill(int id, string name, decimal unit, decimal rate) : base(id, name, unit, rate)
        {
            //Console.WriteLine("i am water bill ctor");
        }
        public override decimal CalculateBillAmount() 
        {
            return UnitsConsumed * RatePerUnit;
        }
        public override decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.02m; 
        }
    }

    class GasBill : UtilityBill
    {
        public GasBill(int id, string name, decimal unit, decimal rate) : base(id, name, unit, rate)
        {
            //Console.WriteLine("i am gas bill ctor");
        }
        public override decimal CalculateBillAmount() 
        {
            return (UnitsConsumed * RatePerUnit) + 150;
        }
        public override decimal CalculateTax(decimal billAmount)
        {
            return 0m;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<UtilityBill> list = new List<UtilityBill>
            {
                new ElectricityBill(101, "A", 350, 6.5m),
                new WaterBill(102, "B", 120, 2.0m),
                new GasBill(103, "C", 30, 8.0m)
            };
            foreach (UtilityBill bill in list)
            {
                bill.PrintBill();
            }
        }
    }
}
