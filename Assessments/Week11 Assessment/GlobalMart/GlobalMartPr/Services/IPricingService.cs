namespace GlobalMartPr.Services
{
    public interface IPricingService
    {
        public decimal CalculatePrice(decimal baseP, string promo);
    }
}
