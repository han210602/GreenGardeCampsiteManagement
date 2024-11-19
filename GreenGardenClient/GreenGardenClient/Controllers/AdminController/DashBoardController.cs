using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GreenGardenClient.Controllers.AdminController
{
    public class DashBoardController : Controller
    {
        private HttpClient _httpClient;

        public DashBoardController()
        {
            _httpClient = new HttpClient(); // Use 'this._httpClient' to initialize the private field
        }
        private T GetDataFromApi<T>(string url)
        {

            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<T>().Result;
        }
        public IActionResult Index(int month)
        {
            try
            {
                if (month == null)
                {
                    month = 0;
                }
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                ProfitVM profitVM = GetDataFromApi<ProfitVM>($"https://localhost:7298/api/DashBoard/GetProfit/{month}");
                List<Account> userdata = GetDataFromApi<List<Account>>("https://localhost:7298/api/DashBoard/GetListCustomer\r\n");

                ViewBag.listuser = userdata;

                return View(profitVM);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
