using Microsoft.AspNetCore.Mvc;
using VoltGearSystem.Models;
using VoltGearSystem.Services;

namespace VoltGearSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly LaptopService _laptopService;

        public InventoryController(LaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var laptops = await _laptopService.GetAsync();
            return View(laptops);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Laptop newLaptop)
        {
            if (ModelState.IsValid)
            {
                await _laptopService.CreateAsync(newLaptop);
                TempData["SuccessMessage"] = "Laptop successfully saved to MongoDB.";
                return RedirectToAction(nameof(Index));
            }

            return View(newLaptop);
        }
    }
}
