namespace GlobalMartPr.Services
{
    public class PricingService : IPricingService
    {
        public decimal CalculatePrice(decimal baseP, string promo)
        {
            decimal price = baseP;
            if (!string.IsNullOrEmpty(promo))
            {
                switch (promo)
                {
                    case "WINTER25":
                        price = baseP * 0.85m;
                        break;
                    case "FREESHIP":
                        price = baseP - 5m;
                        break;
                }
            }
             return price;
        }
    }
}
