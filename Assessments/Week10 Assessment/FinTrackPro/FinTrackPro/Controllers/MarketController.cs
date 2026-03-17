using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class MarketController : Controller
    {
        [HttpGet("Analyze/{ticker}/{days:int?}")]
        public IActionResult Analyze(string ticker, int? days)
        {
            if (days == null)
            {
                days = 30;
            }

            ViewBag.Ticker = ticker;
            ViewBag.Days = days;

            return View();
        }
        public IActionResult Summary()
        {
            //string marketStatus = "open";
            ViewBag.MarketStatus = "Open";

            //string topGainer = "TS";
            ViewData["TopGainer"] = "TS";

            //long volume = 150000000;
            ViewData["Volume"] = 1500000;
            return View();
        }
    }
}
