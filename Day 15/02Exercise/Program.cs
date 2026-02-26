namespace _02Exercise
{
    class Order
    {
        public string Status;
        private bool discount = false;

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _customerName = value;
                }
                else
                {
                    Console.WriteLine("Wrong Customer Name");
                }
            }
        }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get { return _totalAmount; }

        }

        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public Order()
        {
            _customerName = "not available";
            Status = "NEW";
        }

        public Order(int orderid, string customername)
        {
            this.OrderId = orderid;
            this.CustomerName = customername;
            Status = "NEW";
        }

        public void AddItem(decimal price)
        {
            if (price < 0) Console.WriteLine("Amount can't be negative");
            else _totalAmount += price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            if (!discount && percentage <= 30 && percentage >= 0)
            {
                _totalAmount -= ((percentage / 100) * this.TotalAmount);
                discount = true;
            }
            else
            {
                Console.WriteLine("Discount Already Applied");
            }
        }

        public void GetOrderSummary()
        {
            Console.WriteLine($"Order Id = {_orderId}");
            Console.WriteLine($"Customer = {_customerName}");
            Console.WriteLine($"Total Amount = {_totalAmount}");
            Console.WriteLine($"Status = {Status}");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
            {
                Console.WriteLine("ORDER 1");
                Order order1 = new Order(201, "Sneha");
                order1.AddItem(500);
                order1.AddItem(300);
                order1.ApplyDiscount(10);
                order1.ApplyDiscount(20);
                order1.GetOrderSummary();

                Console.WriteLine("ORDER 2");
                Order order2 = new Order();
                order2.GetOrderSummary();

                Console.WriteLine("ORDER 3");
                Order order3 = new Order(202, null);
                order3.GetOrderSummary();

                Console.WriteLine("ORDER 4");
                Order order4 = new Order(203, "Harpuneet");
                order4.AddItem(500);
                order4.AddItem(789);
                order4.ApplyDiscount(10);
                order4.GetOrderSummary();
            }
    }
}
