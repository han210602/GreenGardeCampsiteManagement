using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GreenGardenClient.Controllers
{
    public class CommonController : Controller
    {
        [BindProperty]
        public IFormFile ProfilePictureUrl { get; set; }
        private readonly IHttpClientFactory _clientFactory;

        public CommonController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Utilities()
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
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("EmailError", "Vui lòng nhập email của bạn");

            }
            else if (!Regex.IsMatch(email, emailRegex))
            {
                ModelState.AddModelError("EmailError", "Sai địng dạng email");
            }
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("PasswordError", "Vui lòng nhập mật khẩu của bạn");

            }

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
                        HttpContext.Session.SetString("Password", loginResponse.Password);
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
                    ModelState.AddModelError("PasswordError", "Email hoặc mật khẩu của bị sai. Vui lòng nhập lại");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi ngoại lệ: {ex.Message}");
            }
            ViewBag.email = email;
            ViewBag.pass = password;
            return View(new Login { Email = email, Password = password });
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View(new UserRegisterVM());
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
            ViewBag.code = VerificationCode;
            // Ensure that the model is valid
            if (VerificationCode.IsNullOrEmpty())
            {
                // Log validation errors for debugging
                ViewBag.error = $"Mã xác thực không được để trống";

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
                        ViewBag.error = $"Đăng kí thất bại.";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    ViewBag.error = $"ModelState.AddModelError(string.Empty, \"Xảy ra lỗi trong quá trình đăng kí, Bạn có thể đăng kí lại\");\r\n";

                    return View(model);
                }
            }
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue)
                {
                    return RedirectToAction("Error");
                }

                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("EmailError", "Vui lòng nhập email của bạn");
                    return View(new { Email = email });
                }

                var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailRegex))
                {
                    ModelState.AddModelError("EmailError", "Email không đúng định dạng");
                    return View(new { Email = email });
                }
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync($"https://localhost:7298/api/Account/SendResetPasswordEmail/{email}", null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Đã gửi email đặt lại mật khẩu. Vui lòng kiểm tra email của bạn.";
                    return RedirectToAction("Login"); // Redirect to the Login page after success
                }
                else
                {
                    ModelState.AddModelError("EmailError", "Email của bạn chưa được đăng kí tài khoản");
                    return View(new { Email = email });
                }
            }
            catch (Exception ex)
            {
                TempData["Success"] = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return View(new { Email = email }); // Giữ lại giá trị email trong trường hợp có lỗi
        }


        public IActionResult ChangePassword()
        {
            return View(new ChangePassword());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Error");
            }
            var oldPassword = HttpContext.Session.GetString("Password");
            if (oldPassword == null || oldPassword != model.OldPassword)
            {
                ModelState.AddModelError("OldPassword", "Mật khẩu hiện tại không đúng.");
                return View(model);
            }
            // Kiểm tra mật khẩu mới và xác nhận mật khẩu có khớp không
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return View(model);
            }
            if (model.NewPassword == model.OldPassword)
            {
                ModelState.AddModelError("NewPassword", "Mật khẩu mới vui lòng không trùng với mật khẩu cũ");
                return View(model);
            }
            // Lấy userId từ session


            // Lấy mật khẩu cũ từ session và kiểm tra


            // Chuẩn bị request để gọi API
            var request = new ChangePassword
            {
                UserId = userId.Value,
                OldPassword = oldPassword,
                NewPassword = model.NewPassword,
                ConfirmPassword = model.ConfirmPassword
            };

            string apiUrl = "https://localhost:7298/api/Account/ChangePassword";

            try
            {
                var client = _clientFactory.CreateClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Thay đổi mật khẩu thành công!";
                    HttpContext.Session.SetString("Password", model.NewPassword);
                    return RedirectToAction("UpdateProfile");
                }
                else
                {
                    TempData["Error"] = "Thay đổi mật khẩu thất bại!";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi hệ thống: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Event()
        {
            var events = await GetDataFromApiAsync<List<EventVM>>("https://localhost:7298/api/Event/GetAllEvents");

            ViewBag.Event = events;


            return View("Event");
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
                return RedirectToAction("Error");
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
        public async Task<IActionResult> ChangeProfile(UpdateProfile updateProfile, IFormFile ProfilePictureUrl, string CurrentPictureUrl)
        {


            if (ProfilePictureUrl != null)
            {
                // Lấy tên file từ tệp được tải lên
                string fileName = ProfilePictureUrl.FileName;

                // Sử dụng Stream để tải trực tiếp lên Cloudinary
                using (var stream = ProfilePictureUrl.OpenReadStream())
                {
                    // Cấu hình tài khoản Cloudinary
                    var accountVM = new AccountVM
                    {
                        CloudName = "dxpsghdhb", // Thay bằng giá trị thực tế
                        ApiKey = "312128264571836",
                        ApiSecret = "nU5ETmubjnFSHIcwRPIDjjjuN8Y"
                    };

                    // Ánh xạ từ AccountVM sang Account
                    var account = new CloudinaryDotNet.Account(
                        accountVM.CloudName,
                        accountVM.ApiKey,
                        accountVM.ApiSecret
                    );

                    // Tạo đối tượng Cloudinary
                    var cloudinary = new Cloudinary(account);

                    // Thiết lập thông số upload
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileName, stream), // Đặt stream và tên file
                        PublicId = "Avatar/" + Path.GetFileNameWithoutExtension(fileName), // Tùy chọn đặt tên file trên Cloudinary
                        Folder = "Avatar", // Thư mục trong Cloudinary (tùy chọn)
                    };

                    // Thực hiện upload
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    // Cập nhật đường dẫn hình ảnh từ Cloudinary vào model
                    updateProfile.ProfilePictureUrl = uploadResult.SecureUrl.AbsoluteUri;
                }
            }
            else
            {
                // Sử dụng ảnh hiện tại nếu không có ảnh mới
                updateProfile.ProfilePictureUrl = CurrentPictureUrl;
            }

            // Retrieve the userId from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Check if userId is available; if not, redirect to login
            if (userId == null)
            {
                return RedirectToAction("Error");
            }

            // Ensure the submitted updateProfile model is valid
            //if (!ModelState.IsValid)
            //    {
            //    TempData["Error"] = "Thông tin không hợp lệ.";
            //    return RedirectToAction("UpdateProfile"); // Return to UpdateProfile view with an error message
            //}

            // Prepare the API URL for updating the profile
            string apiUrl = "https://localhost:7298/api/Account/UpdateProfile";

            try
            {
                var client = _clientFactory.CreateClient();
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

                    HttpContext.Session.SetString("Img", updateProfile.ProfilePictureUrl);

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
        [HttpGet("EventDetail")]
        public async Task<IActionResult> EventDetail(int eventId)
        {


            string apiUrl = $"https://localhost:7298/api/Event/GetEventById?eventId={eventId}";

            try
            {
                // Use GetDataFromApiAsync to fetch user profile data
                var events = await GetDataFromApiAsync<EventVM>(apiUrl);

                if (events != null)
                {
                    // Use ViewData instead of ViewBag if you prefer type safety
                    ViewBag.Event = events;
                    return View("EventDetail", events); // Load UpdateProfile view with user profile data
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
    }
}


