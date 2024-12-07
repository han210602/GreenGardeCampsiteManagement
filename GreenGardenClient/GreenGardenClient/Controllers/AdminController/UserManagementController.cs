using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GreenGardenClient.Controllers.AdminController
{
    public class UserManagementController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserManagementController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private async Task<T> GetDataFromApiAsync<T>(string url)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return data!;
                }
                else
                {
                    TempData["ErrorMessage"] = $"Không thể lấy dữ liệu từ API: {response.StatusCode}";
                    return default!;
                }
            }
        }

        // Make Index method asynchronous and await GetDataFromApiAsync calls
        public async Task<IActionResult> Index()
        {
            var userRole = HttpContext.Session.GetInt32("RoleId");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            // Awaiting the API calls
            List<OrderVM> orderdata = await GetDataFromApiAsync<List<OrderVM>>("https://be-green.chunchun.io.vn/api/OrderManagement/GetAllOrders");
            List<Account> userdata = await GetDataFromApiAsync<List<Account>>("https://be-green.chunchun.io.vn/api/User/GetAllCustomers");

            if (userdata == null)
            {
                TempData["ErrorMessage"] = "Không thể lấy dữ liệu người dùng";
            }

            // Pass the order data and the user data to the view
            ViewBag.OrderData = orderdata;
            ViewBag.UserData = userdata;

            return View(userdata);
        }



        [HttpPost("BlockUser/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            Console.WriteLine($"Received id: {id}");
            string apiUrl = $"https://be-green.chunchun.io.vn/api/User/BlockUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Chặn người dùng thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi chặn người dùng";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("UnBlockUser/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            Console.WriteLine($"Received id: {id}");
            string apiUrl = $"https://be-green.chunchun.io.vn/api/User/UnBlockUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Mở khoá người dùng thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Failed to Unblock user: {errorMessage}";
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request error: {httpEx.Message}");
                TempData["ErrorMessage"] = "Network error occurred. Please try again.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: {ex.Message}");
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var userRole = HttpContext.Session.GetInt32("RoleId");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Home");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            // Fetch user data from the API asynchronously and await the result
            var user = await GetDataFromApiAsync<Account>($"https://be-green.chunchun.io.vn/api/User/GetUserById/{id}");

            // Check if user data is null and redirect to an error page if not found
            if (user == null)
            {
                return RedirectToAction("Error", "Home"); // Redirect if the user is not found
            }

            // Pass the user data to the view
            return View(user);
        }
    }
}
