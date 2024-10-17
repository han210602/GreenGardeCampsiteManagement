using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Event()
        {
            return View();
        }

    }
}
