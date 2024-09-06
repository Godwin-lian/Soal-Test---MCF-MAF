using BankMegaTechTest_FE;
using BankMegaTechTest_FE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public AuthController(HttpClient httpClient, IOptions<AppSettings> options)
    {
        _httpClient = httpClient;
        _appSettings = options.Value;
    }

    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("AuthToken") != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDTO model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_appSettings.ApiBaseUrl}auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.UserName),
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            HttpContext.Session.SetString("AuthToken", result.Token);
            HttpContext.Session.SetString("UserId", result.UserId);

            return RedirectToAction("Login", "Auth", new { success = true });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return View(model);
        }
    }


}
