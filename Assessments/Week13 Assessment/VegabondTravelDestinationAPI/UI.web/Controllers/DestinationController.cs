using UI.web.Services;
using Microsoft.AspNetCore.Mvc;
using UI.web.Services;

namespace UI.web.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _service;

        public DestinationController(IDestinationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
    }
}