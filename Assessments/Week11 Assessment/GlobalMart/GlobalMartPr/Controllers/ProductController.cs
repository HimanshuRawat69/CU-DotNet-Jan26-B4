using GlobalMartPr.Models;
using GlobalMartPr.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMartPr.Controllers
{
    public class ProductController : Controller
    {
            private readonly IPricingService _pricingService;

            public ProductController(IPricingService pricingService)
            {
                _pricingService = pricingService;
            }

            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Price = 999 },
                new Product { ProductId = 2, Name = "Smartphone", Price = 499 },
                new Product { ProductId = 3, Name = "Headphones", Price = 199 }
            };

        public IActionResult Index()
        {
            //string promo = "WINTER25";

            var discountedProducts = products.Select(p => new Product
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price
            }).ToList();

            return View(discountedProducts);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = products.Find(p => p.ProductId == id.Value);

            if (product == null)
            {
                return NotFound();
            }

            //product.Price = _pricingService.CalculatePrice(product.Price, "WINTER25");

            return View(product);
        }
        }
}
