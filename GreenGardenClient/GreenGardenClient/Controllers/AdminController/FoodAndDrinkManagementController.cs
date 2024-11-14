using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GreenGardenClient.Controllers.AdminController
{

    public class FoodAndDrinkManagementController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        [BindProperty]
        public IFormFile PictureUrl { get; set; }

        // Constructor to inject IHttpClientFactory
        public FoodAndDrinkManagementController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IActionResult Index()
        {
            var foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");
            var categories = GetDataFromApi<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            // Truyền dữ liệu đến view
            ViewBag.Categories = categories;
            return View(foodAndDrinks);
        }

        private T GetDataFromApi<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            // Retrieve JWT token from cookies
            var jwtToken = Request.Cookies["JWTToken"];

            // Check if JWT token is present; if not, return default (null)
            if (string.IsNullOrEmpty(jwtToken))
            {
                return default;
            }

            // Set Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            try
            {
                using var response = client.GetAsync(url).Result;

                // Check if access is forbidden, return default if unauthorized
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return default;
                }

                response.EnsureSuccessStatusCode(); // Throws if not successful

                return response.Content.ReadFromJsonAsync<T>().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default; // Return default value in case of an error
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFoodAndDrink(int id)
        {
            // Fetch event data from the API
            var eventItem = GetDataFromApi<FoodAndDrinkVMNew>($"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinkDetail?itemId={id}");

            if (eventItem == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Fetch the list of categories
            var categories = GetDataFromApi<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");


            // Pass data to View using a ViewModel
            ViewBag.Categories = categories;


            return View(eventItem);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFoodAndDrink(FoodAndDrinkVMNew model, IFormFile PictureUrl, string CurrentPictureUrl)
        {

            if (PictureUrl != null)
            {
                // Lưu file mới
                string filePath = Path.Combine("wwwroot/images", PictureUrl.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureUrl.CopyToAsync(stream);
                }
                model.ImgUrl = PictureUrl.FileName;
            }
            else
            {
                // Sử dụng ảnh hiện tại
                model.ImgUrl = CurrentPictureUrl;
            }

            // Gọi API để cập nhật thông tin món ăn
            string apiUrl = "https://localhost:7298/api/FoodAndDrink/UpdateFoodOrDrink";
            try
            {

                // Gọi API với phương thức PUT
                var response = await _clientFactory.CreateClient().PutAsJsonAsync(apiUrl, model);


                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, chuyển hướng về trang Index
                    return RedirectToAction("UpdateFoodAndDrink");
                }
                else
                {
                    // Thêm lỗi nếu API không trả về thành công
                    ModelState.AddModelError("", "Failed to update event.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Ghi log và hiển thị thông báo lỗi chung
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(model); // Hiển thị lại form để người dùng biết lỗi
            }
        }


        [HttpGet]
        public IActionResult CreateFoodAndDrink()
        {
            var categories = GetDataFromApi<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");



            ViewBag.Categories = categories;


            return View();

        }




        [HttpPost("ChangeFoodStatus/{id}")]
        public async Task<IActionResult> ChangeFoodStatus(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID."); // 400 Bad Request
            }

            string apiUrl = $"https://localhost:7298/api/FoodAndDrink/ChangeFoodStatus?itemId={id}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok("Thay đổi trạng thái thành công");
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, $"Không thể thay đổi trạng thái. API Error: {responseContent}");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Đã xảy ra lỗi khi kết nối với API.");
                }
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateFoodAndDrink(FoodAndDrinkVMNew model, IFormFile PictureUrl)
        {

            if (PictureUrl != null && PictureUrl.Length > 0)
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                StreamContent fileContent = new StreamContent(PictureUrl.OpenReadStream());
                formData.Add(fileContent, "file", PictureUrl.FileName);

                string filePath = Path.Combine("wwwroot/images", PictureUrl.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureUrl.CopyToAsync(fileStream);
                }

                model.ImgUrl = PictureUrl.FileName;
            }
            else
            {
                ModelState.AddModelError("PictureUrl", "File ảnh không hợp lệ hoặc không được chọn.");
            }

            // Gửi dữ liệu đến API
            string apiUrl = "https://localhost:7298/api/FoodAndDrink/AddFoodOrDrink";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Dữ liệu gửi
                    var requestData = new
                    {
                        ItemName = model.ItemName ?? throw new ArgumentNullException("ItemName"),
                        Description = model.Description,
                        ImgUrl = model.ImgUrl,
                        Price = model.Price > 0 ? model.Price : throw new ArgumentException("Price must be greater than 0"),
                        CategoryId = model.CategoryId > 0 ? model.CategoryId : throw new ArgumentException("Invalid CategoryId"),
                        QuantityAvailable = model.QuantityAvailable >= 0 ? model.QuantityAvailable : throw new ArgumentException("Invalid QuantityAvailable")
                    };


                    // Gửi POST request
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    // Kiểm tra phản hồi
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Món ăn đã được thêm thành công.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Error Response: {response.StatusCode} - {responseContent}");
                        ModelState.AddModelError("", "Không thể thêm món ăn. Vui lòng thử lại.");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi kết nối API.");
                    return View(model);
                }

            }
        }
    }
}




