using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Software()
        {
            return View();
        }
        public IActionResult Tools()
        {
            return View();
        }
    }
}
