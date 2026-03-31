using GlobalMartPr.Models;
using GlobalMartPr.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMartPr.Controllers
{
    public class CartController : Controller
    {
        private readonly IPricingService _pricingService;

        public CartController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }
        private static Cart cart = new Cart(); 

        List<Product> products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Laptop", Price = 999 },
            new Product { ProductId = 2, Name = "Smartphone", Price = 499 },
            new Product { ProductId = 3, Name = "Headphones", Price = 199 }
        };

        public IActionResult Index()
        {
            return View(cart);
        }
        public IActionResult AddToCart(int id)
        {
            var product = products.Find(p => p.ProductId == id);

            if (product != null)
            {
                string promo = "WINTER25";
                var discountedPrice = _pricingService.CalculatePrice(product.Price, promo);
                cart.Items.Add(new Product {
                    ProductId = id,
                    Name = product.Name,
                    Price = discountedPrice });
            }

            return RedirectToAction("Index");
        }
    }
}