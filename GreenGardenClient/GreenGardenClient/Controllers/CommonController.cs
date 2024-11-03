using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace GreenGardenClient.Controllers
{
    public class CommonController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CommonController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
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
                    var loginResponse = JsonSerializer.Deserialize<LoginVM>(responseData);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        // Save token in cookie
                        Response.Cookies.Append("JWTToken", loginResponse.Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTimeOffset.Now.AddHours(1)
                        });
                        HttpContext.Session.SetInt32("RoleId", loginResponse.RoleId);
                        HttpContext.Session.SetInt32("UserId", loginResponse.UserId);
                        HttpContext.Session.SetString("Email", loginResponse.Email);
                        HttpContext.Session.SetString("NumberPhone", loginResponse.Phone);
                        HttpContext.Session.SetString("Pasword", loginResponse.Password);
                        HttpContext.Session.SetString("Fullname", loginResponse.FullName);
                        if (loginResponse.ProfilePictureUrl != null)
                        {
                            HttpContext.Session.SetString("Img", loginResponse.ProfilePictureUrl);
                        }
                        TempData["SuccessMessage"] = "Đăng nhập thành công!";
                        return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Register([FromForm] string VerificationCode, [FromForm] UserRegisterVM model)
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
                        return RedirectToAction("Login", "Common");
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

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Message = "Vui lòng nhập email.";
                return View("ResetPassword");
            }

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync($"https://localhost:7298/api/Account/SendResetPasswordEmail/{email}", null);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Đã gửi email đặt lại mật khẩu. Vui lòng kiểm tra email của bạn.";
                    return RedirectToAction("Login"); // Redirect to the Login page after success
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

            return View("ResetPassword");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPassword(ChangePassword dto)
        {
            // Check if the new password and confirmation match
            if (dto.NewPassword != dto.ConfirmPassword)
            {
                TempData["Error"] = "Mật khẩu không khớp!";
                return RedirectToAction("ChangePassword");
            }

            // Retrieve user ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
                return RedirectToAction("ChangePassword");
            }

            // Validate old password from session
            var oldPassword = HttpContext.Session.GetString("Password");
            if (oldPassword == null || oldPassword != dto.OldPassword)
            {
                TempData["Error"] = "Mật khẩu cũ không khớp!";
                return RedirectToAction("ChangePassword");
            }

            // Prepare the request object with the user's ID and passwords
            var request = new ChangePassword
            {
                UserId = userId.Value,
                OldPassword = dto.OldPassword,
                NewPassword = dto.NewPassword,
                ConfirmPassword = dto.ConfirmPassword
            };

            // Prepare the API URL
            string apiUrl = "https://localhost:7298/api/Account/ChangePassword";

            try
            {
                var client = _clientFactory.CreateClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Thay đổi mật khẩu thành công!";
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Lỗi khi cập nhật thông tin: {errorMessage}";
                    return RedirectToAction("ChangePassword");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("ChangePassword");
            }
        }
        public IActionResult Event()
        {
            return View();
        }
        private async Task<T> GetDataFromApiAsync<T>(string apiUrl)
        {
            using (var client = _clientFactory.CreateClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadFromJsonAsync<T>();
                        return data!;
                    }
                    else
                    {
                        // Log the error here if necessary
                        TempData["ErrorMessage"] = $"Không thể lấy dữ liệu từ API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                        return default!;
                    }
                }
                catch (Exception ex)
                {
                    // Log exception details for debugging
                    TempData["ErrorMessage"] = $"Lỗi kết nối với API: {ex.Message}";
                    return default!;
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            // Retrieve the userId from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Check if userId is available; if not, redirect to login
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để cập nhật hồ sơ.";
                return RedirectToAction("Login", "Common");
            }

            string apiUrl = $"https://localhost:7298/api/Account/GetAccountById?id={userId}";

            try
            {
                // Use GetDataFromApiAsync to fetch user profile data
                var userProfile = await GetDataFromApiAsync<Account>(apiUrl);

                if (userProfile != null)
                {
                    // Use ViewData instead of ViewBag if you prefer type safety
                    ViewBag.UserProfile = userProfile;
                    return View("UpdateProfile", userProfile); // Load UpdateProfile view with user profile data
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể lấy dữ liệu từ API.";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                // Log exception details for debugging
                TempData["ErrorMessage"] = $"Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangeProfile(UpdateProfile updateProfile)
        {
            // Retrieve the userId from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Check if userId is available; if not, redirect to login
            if (userId == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để cập nhật hồ sơ.";
                return RedirectToAction("Login", "Common");
            }

            // Ensure the submitted updateProfile model is valid
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Thông tin không hợp lệ.";
                return RedirectToAction("UpdateProfile"); // Return to UpdateProfile view with an error message
            }

            // Prepare the API URL for updating the profile
            string apiUrl = "https://localhost:7298/api/Account/UpdateProfile";

            try
            {
                var client = _clientFactory.CreateClient();
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Serialize the updateProfile object into JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(updateProfile), Encoding.UTF8, "application/json");

                // Send the POST request to update the profile
                var response = await client.PostAsync(apiUrl, jsonContent); // Use PutAsync if your API expects PUT for updates

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Cập nhật hồ sơ thành công!";
                    HttpContext.Session.SetString("Fullname", updateProfile.FirstName + " " + updateProfile.LastName);
                    HttpContext.Session.SetString("Email", updateProfile.Email);
                    HttpContext.Session.SetString("NumberPhone", updateProfile.PhoneNumber);
                    return RedirectToAction("UpdateProfile"); // Redirect to UpdateProfile after successful update
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Lỗi khi cập nhật thông tin: {errorMessage}";
                    return RedirectToAction("UpdateProfile"); // Redirect to UpdateProfile on error
                }
            }
            catch (Exception ex)
            {
                // Log exception details for debugging
                TempData["Error"] = $"Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("UpdateProfile"); // Redirect to UpdateProfile on exception
            }
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }
    }
}
