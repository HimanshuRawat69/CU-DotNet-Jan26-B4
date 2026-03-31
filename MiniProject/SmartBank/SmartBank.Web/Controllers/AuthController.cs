using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Models;
using System.Text;

using System.Text.Json;
public class AuthController : Controller
{
    private readonly HttpClient _client;

    public AuthController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://localhost:7001/");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/auth/register", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Registration successful! Please login with your credentials.";
            return RedirectToAction("Login");
        }

        ViewBag.Error = await response.Content.ReadAsStringAsync();
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (TempData["Success"] != null)
            ViewBag.Success = TempData["Success"];
            
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/auth/login", content);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid login";
            return View(model);
        }

        var result = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(result);
        var token = jsonDocument.RootElement.GetProperty("token").GetString();
        HttpContext.Session.SetString("JwtToken", token);

        return RedirectToAction("Index", "Account");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}