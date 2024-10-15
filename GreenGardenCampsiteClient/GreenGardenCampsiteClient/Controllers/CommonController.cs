using GreenGardenCampsiteClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GreenGardenCampsiteClient.Controllers
{
    public class CommonController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CommonController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> BookingFoodAndDrink()
        {
            var foodAndDrink = await GetDataFromApiAsync<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            // Lấy danh sách danh mục thiết bị cắm trại
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;  // Thêm danh sách danh mục vào ViewBag

            return View();
            
        }
        [HttpGet]
        [Route("booking-food-drink")]
        public async Task<IActionResult> BookingFoodAndDrink(int? categoryId, int? sortBy)
        {
            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            string apiUrl = $"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinksBySort?";

            if (categoryId.HasValue)
            {
                apiUrl += $"categoryId={categoryId.Value}&";
            }

            if (sortBy.HasValue)
            {
                apiUrl += $"sortBy={sortBy.Value}";
            }

            var foodAndDrink = await GetDataFromApiAsync<List<FoodAndDrinkVM>>(apiUrl);
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");
            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0";
            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;

            return View();
        }

        public IActionResult BookingTicket()
        {
            return View();
        }


        public async Task<IActionResult> BookingCampingGear()
        {
            // Lấy danh sách thiết bị cắm trại
            var campingGears = await GetDataFromApiAsync<List<CampingGearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");

            // Lấy danh sách danh mục thiết bị cắm trại
            var campingCategories = await GetDataFromApiAsync<List<CampingCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.CampingGears = campingGears;
            ViewBag.CampingCategories = campingCategories;  // Thêm danh sách danh mục vào ViewBag

            return View();
        }
        [HttpGet]
        [Route("booking-camping-gear")]
        public async Task<IActionResult> BookingCampingGear(int? categoryId, int? sortBy)
        {
            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            string apiUrl = $"https://localhost:7298/api/CampingGear/GetCampingGearsBySort?";

            if (categoryId.HasValue)
            {
                apiUrl += $"categoryId={categoryId.Value}&";
            }

            if (sortBy.HasValue)
            {
                apiUrl += $"sortBy={sortBy.Value}";
            }

            var campingGears = await GetDataFromApiAsync<List<CampingGearVM>>(apiUrl);
            var campingCategories = await GetDataFromApiAsync<List<CampingCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");
            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0";
            ViewBag.CampingGears = campingGears;
            ViewBag.CampingCategories = campingCategories;

            return View();
        }





        private async Task<T> GetDataFromApiAsync<T>(string apiUrl)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync(apiUrl);
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

        public IActionResult ResetPassword()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            // Tạo HttpClient
            HttpClient _httpClient = new HttpClient();

            // Lấy token JWT từ cookie
            var jwtToken = Request.Cookies["JWTToken"];

            if (string.IsNullOrEmpty(jwtToken))
            {
                TempData["ErrorMessage"] = "Bạn chưa đăng nhập!";
                return RedirectToAction("Login", "Account");
            }

            // Thêm JWT token vào header của yêu cầu
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            // Gửi yêu cầu GET đến API
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7298/api/Account/GetAllAccounts");

            // Kiểm tra xem phản hồi có thành công không
            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu từ API và chuyển đổi sang danh sách các tài khoản
                var accounts = await response.Content.ReadFromJsonAsync<List<Account>>();

                // Gửi dữ liệu đến view để hiển thị
                return View(accounts);
            }
            else
            {
                // Xử lý lỗi nếu yêu cầu không thành công
                TempData["ErrorMessage"] = $"Không thể lấy dữ liệu: {response.StatusCode}";
                return View(new List<Account>());
            }
        }




        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _clientFactory.CreateClient();

            string apiUrl = "https://localhost:7298/api/Account/Login";
            var loginData = new { Email = email, Password = password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            try
            {
                // Send POST request
                var response = await client.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseData);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        // Save token in cookie
                        Response.Cookies.Append("JWTToken", loginResponse.Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTimeOffset.Now.AddHours(1)
                        });

                        TempData["SuccessMessage"] = "Đăng nhập thành công!";
                        return RedirectToAction("Home", "Common");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Phản hồi từ API không hợp lệ.");
                    }
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Lỗi khi đăng nhập: {response.StatusCode} - {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi ngoại lệ: {ex.Message}");
            }

            return View();
        }

        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            try
            {
                var client = _clientFactory.CreateClient();
                string apiUrl = "https://localhost:7298/api/Account/SendVerificationCode";

                var jsonContent = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("VerificationCode", "123456"); // Bạn sẽ lấy mã xác thực thực tế từ response của API
                    return Ok(new { message = "Verification code sent!" });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] string VerificationCode, [FromForm] UserRegistrationModel model)
        {
            // Ensure that the model is valid
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            // Ensure that the email is not null or empty
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email is required.");
                return View(model);
            }

            // Ensure that the passwords match (password confirmation logic)
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return View(model);
            }

            // API endpoint with verification code included as a query parameter
            string apiUrl = $"https://localhost:7298/api/Account/Register?enteredCode={VerificationCode}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Prepare the request object without the ConfirmPassword
                    var requestData = new
                    {
                        model.FirstName,
                        model.LastName,
                        model.Email,
                        model.Password,
                        model.PhoneNumber,

                        // Don't include ConfirmPassword here
                    };


                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Home", "Common");
                    }
                    else
                    {
                        Console.WriteLine($"API Error Response: {response.StatusCode} - {responseContent}");

                        ModelState.AddModelError(string.Empty, $"Registration failed: {responseContent}");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                    return View(model);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Message = "Vui lòng nhập email.";
                return View("ResetPassword"); // Return to the ResetPassword view with the message
            }

            try
            {
                // Create an HttpClient instance using _clientFactory
                var client = _clientFactory.CreateClient();

                // Call the API to send the reset password email
                var response = await client.PostAsync($"https://localhost:7298/api/Account/SendResetPasswordEmail/{email}", null);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Đã gửi email đặt lại mật khẩu. Vui lòng kiểm tra email của bạn.";
                    return RedirectToAction("Home"); // Redirect to Home if the email was sent successfully
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.Message = $"Không thể gửi email đặt lại mật khẩu: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return View("ResetPassword"); // Return to ResetPassword view with the error message if needed
        }
       
        public IActionResult BookingCombo()
        {

            return View();

        }
    }
}
