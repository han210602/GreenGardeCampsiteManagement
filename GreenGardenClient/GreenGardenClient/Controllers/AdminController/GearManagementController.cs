using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace GreenGardenClient.Controllers.AdminController
{

        public class GearManagementController : Controller
        {
            private readonly IHttpClientFactory _clientFactory;
            [BindProperty]
            public IFormFile PictureUrl { get; set; }

            // Constructor to inject IHttpClientFactory
            public GearManagementController(IHttpClientFactory clientFactory)
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
            var gear = await GetDataFromApiAsync<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");

            ViewBag.CampingGear = gear;


            return View("Index");
        }
        [HttpGet("GearDetail")]
        public async Task<IActionResult> GearDetail(int gearId)
        {
            var apiUrl = $"https://localhost:7298/api/CampingGear/GetCampingGearDetail?id={gearId}";

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
                var gear = JsonConvert.DeserializeObject<GearDetailVM>(content);

                // Xác nhận dữ liệu từ API
                if (gear == null)
                {
                    TempData["ErrorMessage"] = "Invalid data received from API!";
                    return RedirectToAction("Error");
                }

                // Gọi API lấy danh sách thể loại thiết bị
                var campingCategories = await GetDataFromApiAsync<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

                // Gán dữ liệu vào ViewBag để truyền sang View
                ViewBag.CampingGear = gear;
                ViewBag.CampingCategories = campingCategories;

                // Trả về View với dữ liệu
                return View("GearDetail", gear);
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
        public async Task<IActionResult> UpdateGear(UpdateGearVM model, IFormFile PictureUrl, string CurrentPictureUrl)
        {

            if (PictureUrl != null)
            {
                // Lưu file mới
                string filePath = Path.Combine("wwwroot/images/Gear", PictureUrl.FileName);
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
            string apiUrl = "https://localhost:7298/api/CampingGear/UpdateCampingGear";
            try
            {

                // Gọi API với phương thức PUT
                var response = await _clientFactory.CreateClient().PutAsJsonAsync(apiUrl, model);


                if (response.IsSuccessStatusCode)
                {
                    TempData["Notification"] = "Thiết bị đã được thay đổi thành công!";
                    // Nếu thành công, chuyển hướng về trang Index
                    return RedirectToAction("GearDetail", new { gearId = model.GearId });
                }
                else
                {
                    // Thêm lỗi nếu API không trả về thành công
                    ModelState.AddModelError("", "Failed to update event.");
                    return View(model); // Hiển thị lại form để người dùng biết lỗi
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
        public async Task<IActionResult> ChangeStatus(int gearId)
        {
            string apiUrl = $"https://localhost:7298/api/CampingGear/ChangeGearStatus?gearId={gearId}";

            try
            {
                // Gọi API để thay đổi trạng thái thiết bị
                var response = await _clientFactory.CreateClient().PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, chuyển hướng về trang chi tiết thiết bị
                    TempData["Notification"] = "Trạng thái thiết bị đã được thay đổi thành công!";
                    return RedirectToAction("GearDetail", new { gearId });
                }
                else
                {
                    // Thêm lỗi nếu API không trả về thành công
                    TempData["Notification"] = "Không thể thay đổi trạng thái thiết bị.";
                    return RedirectToAction("GearDetail", new { gearId });
                }
            }
            catch (Exception ex)
            {
                // Ghi log và hiển thị thông báo lỗi chung
                TempData["Notification"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("GearDetail", new { gearId });
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateGear()
        {
            var campingCategories = await GetDataFromApiAsync<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            // Gán lại danh sách danh mục cho ViewBag
            ViewBag.CampingCategories = campingCategories;

            return View(new AddGearVM());
        }
          
        

        [HttpPost]
        public async Task<IActionResult> CreateGear(AddGearVM model, IFormFile PictureUrl)
        {
            var campingCategories = await GetDataFromApiAsync<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            // Gán lại danh sách danh mục cho ViewBag
            ViewBag.CampingCategories = campingCategories;
            // Set default values for CreatedAt and Status
            model.CreatedAt = DateTime.Now;
            model.Status = model.Status ?? true;  // Set to true if not provided
            ViewBag.GearCategoryId = model.GearCategoryId;
           
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


            // Prepare request data for API
            string apiUrl = "https://localhost:7298/api/CampingGear/AddCampingGear";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var requestData = new
                    {
                        GearName = model.GearName ,
                        Description = model.Description,
                        ImgUrl = model.ImgUrl,
                        RentalPrice = model.RentalPrice ,
                        GearCategoryId = model.GearCategoryId,
                        QuantityAvailable = model.QuantityAvailable,
                        Status = model.Status,
                        CreatedAt = model.CreatedAt
                    };

                    // Send POST request
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);
                    
                    ViewBag.Gear = model;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Notification"] = "Thiết bị đã được thêm thành công.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", "Không thể thêm thiết bị. Vui lòng thử lại.");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Đã xảy ra lỗi khi kết nối API: {ex.Message}");
                    return View(model);
                }
            }
        }

    }

}


