using Microsoft.AspNetCore.Mvc;

namespace TrainingPortal.Controllers
{
    // The class name must end with 'Controller'
    public class TrainingController : Controller
    {
        // Action for the Home page
        public IActionResult Index() => View();

        // Action for the Courses page
        public IActionResult Courses() => View();

        // Action for the Contact page
        public IActionResult Contact() => View();
    }
}