using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GreenGardenClient.Controllers.AdminController
{
    public class UserManagementController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserManagementController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Sử dụng phương thức bất đồng bộ (async) để gọi API
        private async Task<T> GetDataFromApiAsync<T>(string url)
        {
            var client = _clientFactory.CreateClient();
            var jwtToken = Request.Cookies["JWTToken"];

            if (string.IsNullOrEmpty(jwtToken))
            {
                return default(T); // Nếu không có token, trả về default
            }

            // Thêm Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            try
            {
                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return default(T);
                }

                response.EnsureSuccessStatusCode(); // Kiểm tra nếu API trả về lỗi

                return await response.Content.ReadFromJsonAsync<T>(); // Đọc nội dung trả về và parse JSON
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default(T); // Trả về giá trị mặc định nếu có lỗi
            }
        }

        // Phương thức Index dùng async để lấy dữ liệu từ API
        public async Task<IActionResult> Index()
        {
            var userRole = HttpContext.Session.GetInt32("RoleId");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || (userRole != 1 && userRole != 2))
            {
                return RedirectToAction("Index", "Home");
            }
            var client = _clientFactory.CreateClient();
            var jwtToken = Request.Cookies["JWTToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            // Gọi dữ liệu bất đồng bộ từ API
            var orderData = await GetDataFromApiAsync<List<OrderVM>>("https://be-green.chunchun.io.vn/api/OrderManagement/GetAllOrders");
            var userData = await GetDataFromApiAsync<List<Account>>("https://be-green.chunchun.io.vn/api/User/GetAllCustomers");

            if (userData == null)
            {
                TempData["ErrorMessage"] = "Không thể lấy dữ liệu người dùng";
            }

            ViewBag.OrderData = orderData;
            ViewBag.UserData = userData;

            return View(userData);
        }

        // Chặn người dùng
        [HttpPost("BlockUser/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            string apiUrl = $"https://be-green.chunchun.io.vn/api/User/BlockUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Chặn người dùng thành công.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi chặn người dùng";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // Mở khoá người dùng
        [HttpPost("UnBlockUser/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            string apiUrl = $"https://be-green.chunchun.io.vn/api/User/UnBlockUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Mở khoá người dùng thành công";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Failed to Unblock user: {errorMessage}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request error: {httpEx.Message}");
                TempData["ErrorMessage"] = "Network error occurred. Please try again.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: {ex.Message}");
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // Cập nhật thông tin người dùng
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var userRole = HttpContext.Session.GetInt32("RoleId");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || (userRole != 1 && userRole != 2))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await GetDataFromApiAsync<Account>($"https://be-green.chunchun.io.vn/api/User/GetUserById/{id}");

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(user);
        }
    }
}
