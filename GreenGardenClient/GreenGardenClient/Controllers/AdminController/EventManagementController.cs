using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GreenGardenClient.Controllers.AdminController
{
    public class EventManagementController : Controller
    {
        [BindProperty]
        public IFormFile PictureUrl { get; set; }
        private readonly IHttpClientFactory _clientFactory;

        public EventManagementController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            var events = GetDataFromApi<List<EventVM>>("https://localhost:7298/api/Event/GetAllEvents");
            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Home");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (events == null)
            {
                return RedirectToAction("Index", "Error");
            }



            return View(events);
        }


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
        [HttpGet]
        public IActionResult CreateEvent()
        {

            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Home");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new EventVM()); // Otherwise, return the view with userdata

        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventVM model)
        {
            if (model.EventDate == null)
            {
                ModelState.AddModelError("EventDate", "Ngày sự kiện không được để trống");
            }
            if (model.StartTime == null)
            {
                ModelState.AddModelError("StartTime", "Thời gian bắt đầu sự kiện không được để trống");
            }
            if (model.EndTime == null)
            {
                ModelState.AddModelError("EndTime", "Thời gian kết thúc sự kiện không được để trống");
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if UserId is not found in session
                return RedirectToAction("Index", "Home");
            }


            if (PictureUrl != null)
            {
                // Lấy tên file từ tệp được tải lên
                string fileName = PictureUrl.FileName;

                // Sử dụng Stream để tải trực tiếp lên Cloudinary
                using (var stream = PictureUrl.OpenReadStream())
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
                        PublicId = "Event/" + Path.GetFileNameWithoutExtension(fileName), // Tùy chọn đặt tên file trên Cloudinary
                        Folder = "Event", // Thư mục trong Cloudinary (tùy chọn)
                    };

                    // Thực hiện upload
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    // Cập nhật đường dẫn hình ảnh từ Cloudinary vào model
                    model.PictureUrl = uploadResult.SecureUrl.AbsoluteUri;
                }
            }
            else
            {
                // Sử dụng ảnh hiện tại nếu không có ảnh mới
                model.PictureUrl = "Colorful Modern Camping Club Logo.png"; // Hoặc bỏ qua giá trị ImgUrl nếu cần
                ModelState.AddModelError("PictureUrl", "File ảnh không hợp lệ hoặc không được chọn.");
            }
            // URL API to add a new event
            string apiUrl = "https://localhost:7298/api/Event/AddEvent";

            using (HttpClient client = new HttpClient())
            {
                try
                {


                    // Get UserId from Session


                    var requestData = new
                    {
                        model.EventName,
                        model.Description,
                        model.EventDate,
                        model.StartTime,
                        model.EndTime,
                        model.Location,
                        model.PictureUrl,
                        CreateBy = userId.Value // Assign CreateBy with the UserId from session
                    };

                    // Send POST request to the API
                    var response = await client.PostAsJsonAsync(apiUrl, requestData);

                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["NotificationSuccess"] = "Sự kiện đã được thêm thành công.";
                        // Redirect to event list or success page
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["NotificationError"] = "Sự kiện đã thêm thất bại.";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    // Log any exception that occurs and redirect to error page
                    Console.WriteLine($"Exception occurred: {ex.Message}");

                    // Redirect to error page in case of an exception
                    return View(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int id)
        {
            // Fetch event data from the API asynchronously
            var eventItem = GetDataFromApi<UpdateEvent>($"https://localhost:7298/api/Event/GetEventById?eventId={id}");
            var userRole = HttpContext.Session.GetInt32("RoleId");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (userRole != 1 && userRole != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            // Check if event data is null and redirect to an error page if not found
            if (eventItem == null)
            {
                return RedirectToAction("Index", "Error"); // Redirect if the event is not found
            }

            var eventList = new List<UpdateEvent> { eventItem };

            // Pass the list to the view
            return View(eventList);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(UpdateEvent model, IFormFile PictureUrl, string CurrentPictureUrl)
        {
            // Kiểm tra nếu có file ảnh mới
            if (PictureUrl != null)
            {
                // Lấy tên file từ tệp được tải lên
                string fileName = PictureUrl.FileName;

                // Sử dụng Stream để tải trực tiếp lên Cloudinary
                using (var stream = PictureUrl.OpenReadStream())
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
                        PublicId = "Event/" + Path.GetFileNameWithoutExtension(fileName), // Tùy chọn đặt tên file trên Cloudinary
                        Folder = "Event", // Thư mục trong Cloudinary (tùy chọn)
                    };

                    // Thực hiện upload
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    // Cập nhật đường dẫn hình ảnh từ Cloudinary vào model
                    model.PictureUrl = uploadResult.SecureUrl.AbsoluteUri;
                }
            }
            else
            {
                // Sử dụng ảnh hiện tại nếu không có ảnh mới
                model.PictureUrl = CurrentPictureUrl;
            }

            // Chuẩn bị dữ liệu để gửi qua API

            string apiUrl = "https://localhost:7298/api/Event/UpdateEvent";
            try
            {

                // Gọi API với phương thức PUT
                var response = await _clientFactory.CreateClient().PutAsJsonAsync(apiUrl, model);


                if (response.IsSuccessStatusCode)
                {
                    TempData["NotificationSuccess"] = "Sự kiện đã được cập nhật thành công.";
                    // Nếu thành công, chuyển hướng về trang Index
                    return RedirectToAction("UpdateEvent");
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




        public async Task<T> GetDataFromApi<T>(string url, object requestData)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            return default;
        }




        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Đường dẫn API để gọi đến API xóa sự kiện
            string apiUrl = $"https://localhost:7298/api/Event/DeleteEvent/{id}";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                // Kiểm tra kết quả trả về từ server
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to delete the event.");
                }
            }
        }


    }
}
