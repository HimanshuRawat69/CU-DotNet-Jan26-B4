using FinTrackPro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class PortfolioController : Controller
    {

        private static List<Asset> stockList = new List<Asset>()
        {
            new Asset
            {
                Id = 1,
                Name = "a",
                Price = 1000
            },
            new Asset
            {
                Id = 2,
                Name = "b",
                Price = 2000
            },
            new Asset
            {
                Id = 3,
                Name = "c",
                Price = 3000
            }
        };
        // GET: PortfolioController
        public ActionResult Index()
        {
            ViewData["Total"] = stockList.Sum(x => x.Price);
            return View(stockList);
        }

        // GET: PortfolioController/Details/5
        [Route("Asset/Info/{id:int}")]
        public ActionResult Details(int id)
        {
            var asset = stockList.Find(a => a.Id == id);
            //if (asset == null) return View(null);
            return View(asset);
        }

        // GET: PortfolioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortfolioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PortfolioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortfolioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PortfolioController/Delete/5
        public ActionResult Delete(int id)
        {
            var asset = stockList.Find(a => a.Id == id);
            if(asset != null)
            {
                stockList.Remove(asset);
                TempData["Message"] = "Stock deleted sucessfully";
            }
            return RedirectToAction("Index");
        }

        // POST: PortfolioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
