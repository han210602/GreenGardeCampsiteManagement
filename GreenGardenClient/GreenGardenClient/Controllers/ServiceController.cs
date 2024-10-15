using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenGardenClient.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IHttpClientFactory _clientFactory; // Đảm bảo bạn đã khai báo IHttpClientFactory

        public ServiceController(ILogger<ServiceController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory; // Khởi tạo IHttpClientFactory
        }
        public IActionResult Index()
        {
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
        public async Task<IActionResult> OrderGear()
        {
            var campingGears = await GetDataFromApiAsync<List<CampingGearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");

            // Lấy danh sách danh mục thiết bị cắm trại
            var campingCategories = await GetDataFromApiAsync<List<CampingCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.CampingGears = campingGears;
            ViewBag.CampingCategories = campingCategories;  // Thêm danh sách danh mục vào ViewBag

            return View();
        }
        [HttpGet]
        [Route("order-gear")]
        public async Task<IActionResult> OrderGear(int? categoryId, int? sortBy)
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

        public IActionResult OrderFoodAndDrink()
        {
            return View();
        }
        public IActionResult FoodDetail()
        {
            return View();
        }
    }
}
