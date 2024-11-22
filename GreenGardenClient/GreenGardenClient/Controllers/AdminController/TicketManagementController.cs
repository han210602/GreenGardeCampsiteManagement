using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GreenGardenClient.Controllers.AdminController
{
    public class TicketManagementController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        [BindProperty]
        public IFormFile PictureUrl { get; set; }

        // Constructor to inject IHttpClientFactory
        public TicketManagementController(IHttpClientFactory clientFactory)
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
            var ticket = await GetDataFromApiAsync<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");
            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Error");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Error");
            }
            ViewBag.Ticket = ticket;


            return View("Index");
        }
        [HttpGet("UpdateTicketDetail")]
        public async Task<IActionResult> UpdateTicketDetail(int ticketId)
        {
            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Error");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Error");
            }
            var apiUrl = $"https://localhost:7298/api/Ticket/GetTicketDetail?id={ticketId}";

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
                var gear = JsonConvert.DeserializeObject<TicketDetailVM>(content);

                // Xác nhận dữ liệu từ API
                if (gear == null)
                {
                    TempData["ErrorMessage"] = "Invalid data received from API!";
                    return RedirectToAction("Error");
                }

                // Gọi API lấy danh sách thể loại thiết bị
                var campingCategories = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");

                // Gán dữ liệu vào ViewBag để truyền sang View
                ViewBag.Ticket = gear;
                ViewBag.TicketCategories = campingCategories;

                // Trả về View với dữ liệu
                return View("UpdateTicketDetail", gear);
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
        public async Task<IActionResult> UpdateTicket(UpdateTicketVM model, IFormFile PictureUrl, string CurrentPictureUrl)
        {

            if (PictureUrl != null)
            {
                // Lưu file mới
                string filePath = Path.Combine("wwwroot/images/Ticket", PictureUrl.FileName);
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
            string apiUrl = "https://localhost:7298/api/Ticket/UpdateTicket";
            try
            {

                // Gọi API với phương thức PUT
                var response = await _clientFactory.CreateClient().PutAsJsonAsync(apiUrl, model);


                if (response.IsSuccessStatusCode)
                {
                    TempData["NotificationSuccess"] = "Vé đã được thay đổi thành công.";
                    // Nếu thành công, chuyển hướng về trang Index
                    return RedirectToAction("UpdateTicketDetail", new { ticketId = model.TicketId });
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
        public async Task<IActionResult> ChangeStatus(int ticketId)
        {
            string apiUrl = $"https://localhost:7298/api/Ticket/ChangeTicketStatus?ticketId={ticketId}";

            try
            {
                // Gọi API để thay đổi trạng thái thiết bị
                var response = await _clientFactory.CreateClient().PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, chuyển hướng về trang chi tiết thiết bị
                    TempData["NotificationSuccess"] = "Trạng thái vé đã được thay đổi thành công!";
                    return RedirectToAction("UpdateTicketDetail", new { ticketId });
                }
                else
                {
                    // Thêm lỗi nếu API không trả về thành công
                    TempData["NotificationError"] = "Không thể thay đổi trạng thái vé.";
                    return RedirectToAction("UpdateTicketDetail", new { ticketId });
                }
            }
            catch (Exception ex)
            {
                // Ghi log và hiển thị thông báo lỗi chung
                TempData["Notification"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("UpdateTicketDetail", new { ticketId });
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateTicket()
        {
            var ticketCategories = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");
            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Error");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Error");
            }
            // Gán dữ liệu vào ViewBag để truyền sang View
            ViewBag.TicketCategories = ticketCategories;


            return View(new AddTicketVM());

        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(AddTicketVM model, IFormFile PictureUrl)
        {
            var ticketCategories = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");

            // Gán dữ liệu vào ViewBag để truyền sang View
            ViewBag.TicketCategories = ticketCategories;
            model.CreatedAt = DateTime.Now;
            model.Status = true;
            if (PictureUrl != null && PictureUrl.Length > 0)
            {
                string filePath = Path.Combine("wwwroot/images/Ticket", PictureUrl.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureUrl.CopyToAsync(fileStream);
                }

                model.ImgUrl = PictureUrl.FileName; // Save image name
            }
            else
            {
                model.ImgUrl = "Colorful Modern Camping Club Logo.png";
                ModelState.AddModelError("PictureUrl", "File ảnh không hợp lệ hoặc không được chọn.");               
            }

            // Prepare request data for API
            string apiUrl = "https://localhost:7298/api/Ticket/AddTicket";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var requestData = new
                    {
                        TicketName = model.TicketName,
                        ImgUrl = model.ImgUrl,
                        Price = model.Price,
                        TicketCategoryId = model.TicketCategoryId,
                        Status = model.Status,
                        CreatedAt = model.CreatedAt
                    };

                    // Send POST request
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["NotificationSuccess"] = "Vé đã được thêm thành công.";
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
