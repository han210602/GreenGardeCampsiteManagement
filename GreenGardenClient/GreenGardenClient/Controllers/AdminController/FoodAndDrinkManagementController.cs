using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        private async Task<T> GetDataFromApiAsync<T>(string apiUrl)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
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
        public async Task<IActionResult> Index()
        {
            var gear = await GetDataFromApiAsync<List<FoodAndDrinkVMNew>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            ViewBag.FoodAndDrink = gear;


            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFoodAndDrink(int itemId)
        {

            var apiUrl = $"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinkDetail?itemId={itemId}";

            try
            {
                // Tạo HttpClient
                var client = _clientFactory.CreateClient();

                // Gọi API để lấy chi tiết thiết bị
                var response = await client.GetAsync(apiUrl);

                // Kiểm tra trạng thái API response
                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve data from API: {response.StatusCode} - {response.ReasonPhrase}";
                    return RedirectToAction("Error");
                }

                // Parse nội dung trả về từ API
                var content = await response.Content.ReadAsStringAsync();
                var gear = JsonConvert.DeserializeObject<FoodAndDrinkVMNew>(content);

                // Xác nhận dữ liệu từ API
                if (gear == null)
                {
                    TempData["ErrorMessage"] = "Invalid data received from API!";
                    return RedirectToAction("Error");
                }

                // Gọi API lấy danh sách thể loại thiết bị
                var campingCategories = await GetDataFromApiAsync<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

                // Gán dữ liệu vào ViewBag để truyền sang View
                ViewBag.CampingGear = gear;
                ViewBag.Categories = campingCategories;

                // Trả về View với dữ liệu
                return View("UpdateFoodAndDrink", gear);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = $"Error connecting to API: {ex.Message}";
                return RedirectToAction("Error");
            }
            catch (JsonException ex)
            {
                TempData["ErrorMessage"] = $"Error parsing JSON response: {ex.Message}";
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Unexpected error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
 

        [HttpPost]
        public async Task<IActionResult> UpdateFoodAndDrink(UpdateFoodAndDrinkVM model, IFormFile PictureUrl, string CurrentPictureUrl)
        {

            if (PictureUrl != null)
            {
                // Lưu file mới
                string filePath = Path.Combine("wwwroot/images/Food", PictureUrl.FileName);
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
                    TempData["Notification"] = "Đồ ăn- đồ uống đã được thay đổi thành công!";
                    // Nếu thành công, chuyển hướng về trang Index
                    return RedirectToAction("UpdateFoodAndDrink", new { itemId = model.ItemId });
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




        [HttpPost] // Dùng POST cho form submit
        public async Task<IActionResult> ChangeStatus(int itemId)
        {
            string apiUrl = $"https://localhost:7298/api/FoodAndDrink/ChangeFoodStatus?itemId={itemId}";

            try
            {
                // Gọi API để thay đổi trạng thái thiết bị
                var response = await _clientFactory.CreateClient().PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, chuyển hướng về trang chi tiết thiết bị
                    TempData["Notification"] = "Trạng thái đồ ăn, đồ uống đã được thay đổi thành công!";
                    return RedirectToAction("UpdateFoodAndDrink", new { itemId });
                }
                else
                {
                    // Thêm lỗi nếu API không trả về thành công
                    TempData["Notification"] = "Không thể thay đổi trạng thái thiết bị.";
                    return RedirectToAction("UpdateFoodAndDrink", new { itemId });
                }
            }
            catch (Exception ex)
            {
                // Ghi log và hiển thị thông báo lỗi chung
                TempData["Notification"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("UpdateFoodAndDrink", new { itemId });
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateFoodAndDrink()
        {
            var categories = await GetDataFromApiAsync<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");



            ViewBag.Categories = categories;


            return View(new AddFoodAndDrinkVM());

        }
        [HttpPost]
        public async Task<IActionResult> CreateFoodAndDrink(AddFoodAndDrinkVM model, IFormFile PictureUrl)
        {
            var categories = await GetDataFromApiAsync<List<CategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");
            ViewBag.Categories = categories;
            model.CreatedAt = DateTime.Now;
            model.Status = model.Status ?? true;

            if (PictureUrl != null && PictureUrl.Length > 0)
            {
                string filePath = Path.Combine("wwwroot/images/Gear", PictureUrl.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureUrl.CopyToAsync(fileStream);
                }
                model.ImgUrl = PictureUrl.FileName; // Save image name
            }
            else
            {
                // Nếu không có ảnh, sử dụng ảnh mặc định hoặc bỏ qua xử lý ảnh
                model.ImgUrl = "Colorful Modern Camping Club Logo.png"; // Hoặc bỏ qua giá trị ImgUrl nếu cần
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
                        ItemName = model.ItemName,
                        Description = model.Description,
                        ImgUrl = model.ImgUrl,
                        Price = model.Price,
                        CategoryId = model.CategoryId,
                        Status = model.Status,
                        CreatedAt = model.CreatedAt
                    };


                    // Gửi POST request
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    // Kiểm tra phản hồi
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Notification"] = "Món ăn đã được thêm thành công.";
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




