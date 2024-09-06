using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankMegaTechTest_FE.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace BankMegaTechTest_FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IOptions<AppSettings> options)
        {
            _logger = logger;
            _httpClient = httpClient;
            _appSettings = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var authToken = HttpContext.Session.GetString("AuthToken");
            if (authToken == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !long.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Error", "Home");
            }

            try
            {
                var locationsResponse = await _httpClient.GetFromJsonAsync<List<LocationDTO>>($"{_appSettings.ApiBaseUrl}Bussiness/location/all");
                if (locationsResponse == null)
                {
                    locationsResponse = new List<LocationDTO>();
                }

                var bpkbsResponse = await _httpClient.GetFromJsonAsync<TrBpkbDTO>($"{_appSettings.ApiBaseUrl}Bussiness/bpkb?userId={userId}");
                if (bpkbsResponse == null)
                {
                    bpkbsResponse = new TrBpkbDTO();
                }

                var detailBpkb = new DetailBpkb
                {
                    Locations = locationsResponse,
                    Bpkb = bpkbsResponse
                };

                return View(detailBpkb);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON deserialization error: {ex.Message}");
                ViewData["Message"] = "An error occurred while loading data.";
                return View(new DetailBpkb());
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertBpkb(DetailBpkb model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Please correct the errors and try again.";
                return View(model);
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            long.TryParse(userIdString, out var userId);

            var request = new TrBpkbDTO
            {
                AgreementNumber = model.Bpkb.AgreementNumber,
                BpkbDate = model.Bpkb.BpkbDate,
                BpkbDateIn = model.Bpkb.BpkbDateIn,
                BpkbNo = model.Bpkb.BpkbNo,
                BranchId = model.Bpkb.BranchId,
                FakturDate = model.Bpkb.FakturDate,
                FakturNo = model.Bpkb.FakturNo,
                LocationId = model.Bpkb.LocationId,
                PoliceNo = model.Bpkb.PoliceNo,
                UserId = userId

            };

            var response = await _httpClient.PostAsJsonAsync($"{_appSettings.ApiBaseUrl}Bussiness/insert", request);
            if (response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "BPKB inserted successfully.";
            }
            else
            {
                ViewData["Message"] = "Failed to insert BPKB.";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBpkb(DetailBpkb model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Please correct the errors and try again.";
                return View(model);
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            long.TryParse(userIdString, out var userId);

            var request = new TrBpkbDTO
            {
                AgreementNumber = model.Bpkb.AgreementNumber,
                BpkbDate = model.Bpkb.BpkbDate,
                BpkbDateIn = model.Bpkb.BpkbDateIn,
                BpkbNo = model.Bpkb.BpkbNo,
                BranchId = model.Bpkb.BranchId,
                FakturDate = model.Bpkb.FakturDate,
                FakturNo = model.Bpkb.FakturNo,
                LocationId = model.Bpkb.LocationId,
                PoliceNo = model.Bpkb.PoliceNo,
                UserId = userId
            };

            var response = await _httpClient.PutAsJsonAsync($"{_appSettings.ApiBaseUrl}Bussiness/update", request);
            if (response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "BPKB updated successfully.";
            }
            else
            {
                ViewData["Message"] = "Failed to update BPKB.";
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
