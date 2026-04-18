using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Services.DTOs;
using System.Net.Http.Json;

namespace NorthwindCatalog.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _client;

        public CategoriesController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<CategoryDto>>("api/categories");
            return View(data);
        }
    }
}
