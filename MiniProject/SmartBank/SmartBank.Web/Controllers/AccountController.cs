using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartBank.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        public AccountController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:7002/");
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync("api/accounts");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var accounts = JsonSerializer.Deserialize<List<AccountViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View(accounts);
                }

                ViewBag.Error = "Failed to load accounts";
                return View(new List<AccountViewModel>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
                return View(new List<AccountViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "You must be logged in to create an account.";
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.PostAsync(
                    "api/accounts",
                    new StringContent("{}", Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Account creation failed: {errorContent}";
                    return RedirectToAction("Index");
                }

                TempData["Success"] = "Account created successfully! Your new bank account is ready to use.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequestViewModel model)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "You must be logged in" });
            }

            // Debug logging
            Console.WriteLine($"Deposit Request - AccountId: {model.AccountId}, Amount: {model.Amount}");

            if (model.AccountId == 0)
            {
                return Json(new { success = false, message = "Invalid account ID. Please refresh the page and try again." });
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(model);
                Console.WriteLine($"Sending to API: {json}");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("api/accounts/deposit", content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Deposit successful!" });
                }

                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = error });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequestViewModel model)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "You must be logged in" });
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("api/accounts/withdraw", content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Withdrawal successful!" });
                }

                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = error });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountDetails(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Unauthorized" });
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync($"api/accounts/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var account = JsonSerializer.Deserialize<AccountViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return Json(new { success = true, account });
                }

                return Json(new { success = false, message = "Account not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}