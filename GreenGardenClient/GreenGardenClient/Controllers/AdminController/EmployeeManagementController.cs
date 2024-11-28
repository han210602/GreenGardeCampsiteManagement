using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GreenGardenClient.Controllers.AdminController
{
    public class EmployeeManagementController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public EmployeeManagementController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Private method to fetch data from API
        private T GetDataFromApi<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            // Retrieve JWT token from cookies
            var jwtToken = Request.Cookies["JWTToken"];

            // Check if JWT token is present; if not, return default (null)
            if (string.IsNullOrEmpty(jwtToken))
            {
                return default(T);
            }

            // Set Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            try
            {
                using var response = client.GetAsync(url).Result;

                // Check if access is forbidden, return default if unauthorized
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return default(T);
                }

                response.EnsureSuccessStatusCode(); // Throws if not successful

                return response.Content.ReadFromJsonAsync<T>().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default(T); // Return default value in case of an error
            }
        }

        // Main action method
        public IActionResult Index()
        {


            var userdata = GetDataFromApi<List<Account>>("https://localhost:7298/api/User/GetAllEmployees");

            // Check if userdata is null, meaning there was an error or forbidden access
            if (userdata == null)
            {
                return RedirectToAction("Error", "Home"); // Redirect to error page if no data
            }

            return View(userdata); // Otherwise, return the view with userdata
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee model)
        {
            string apiUrl = "https://localhost:7298/api/User/AddEmployee";

            if (!ModelState.IsValid)
            {
                // Return to the view with validation errors if the model is not valid
                return View(model);
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Prepare the data to send to the API
                    var requestData = new
                    {
                        model.FirstName,
                        model.LastName,
                        model.Email,
                        model.Password,
                        model.PhoneNumber,
                        model.Address,
                        model.DateOfBirth,
                        model.Gender,
                    };

                    // Send POST request to the API
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    // If the response is not successful
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Error Response: {responseContent}");

                        // Add the error message to ModelState
                        ModelState.AddModelError("Email", "Email đã tồn tại hoặc không đúng định dạng.");

                        // Return to the view with the error
                        return View(model);
                    }

                    // Handle success case
                    TempData["SuccessMessage"] = "Tạo nhân viên thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    return RedirectToAction("Error", "Home");
                }
            }
        }


        [HttpPost("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Console.WriteLine($"Received id: {id}");
            string apiUrl = $"https://localhost:7298/api/User/DeleteUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();


                // Gửi yêu cầu DELETE thay vì POST
                var response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Xoá nhân viên thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error Response: {errorMessage}"); // Log lỗi từ API
                    TempData["ErrorMessage"] = $"Failed to delete user: {errorMessage}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult BlogManagement()
        {
            return View();
        }


        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            // Fetch user data from the API asynchronously
            var user = GetDataFromApi<Account>($"https://localhost:7298/api/User/GetUserById/{id}");

            // Check if user data is null and redirect to an error page if not found
            if (user == null)
            {
                return RedirectToAction("Error", "Home"); // Redirect if the user is not found
            }

            // Pass the user to the view
            return View(user);
        }
        [HttpPost("BlockEmployee/{id}")]
        public async Task<IActionResult> BlockEmployee(int id)
        {
            Console.WriteLine($"Received id: {id}");
            string apiUrl = $"https://localhost:7298/api/User/BlockUser/{id}";

            try
            {
                var client = _clientFactory.CreateClient();


                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Chặn nhân viên thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Failed to block user: {errorMessage}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost("UnBlockEmployee/{id}")]
        public async Task<IActionResult> UnBlockEmployee(int id)
        {
            Console.WriteLine($"Received id: {id}"); // Log nhận ID từ request

            string apiUrl = $"https://localhost:7298/api/User/UnBlockUser/{id}";

            try
            {
                // Tạo HttpClient từ _clientFactory
                var client = _clientFactory.CreateClient();
                Console.WriteLine($"Sending POST request to API: {apiUrl}");

                // Gửi POST request đến API
                var response = await client.PostAsync(apiUrl, null);

                // Kiểm tra trạng thái phản hồi
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("API call succeeded.");
                    TempData["SuccessMessage"] = "Bỏ chặn nhân viên thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Đọc thông báo lỗi từ API
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API call failed with error: {errorMessage}");
                    TempData["ErrorMessage"] = $"Failed to Unblock user: {errorMessage}";
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Lỗi liên quan đến HTTP
                Console.WriteLine($"HTTP Request error: {httpEx.Message}");
                TempData["ErrorMessage"] = "Network error occurred. Please try again.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Lỗi chung
                Console.WriteLine($"Internal server error: {ex.Message}");
                TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
