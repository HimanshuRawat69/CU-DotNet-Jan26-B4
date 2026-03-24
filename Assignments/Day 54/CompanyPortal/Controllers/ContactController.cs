using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
