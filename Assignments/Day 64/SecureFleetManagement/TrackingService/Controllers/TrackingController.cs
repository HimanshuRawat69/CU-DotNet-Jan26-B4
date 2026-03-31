using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TrackingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : Controller
    {
        [HttpGet("gps")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetGps()
        {
            return Ok("Sensitive GPS Data");
        }
    }
}
