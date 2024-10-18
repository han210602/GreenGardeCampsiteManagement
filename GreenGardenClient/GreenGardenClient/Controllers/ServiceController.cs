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
            var campingGears = await GetDataFromApiAsync<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");

            // Lấy danh sách danh mục thiết bị cắm trại
            var campingCategories = await GetDataFromApiAsync<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.CampingGears = campingGears;
            ViewBag.CampingCategories = campingCategories;  // Thêm danh sách danh mục vào ViewBag

            return View();
        }
        [HttpGet]
        [Route("order-gear")]
        public async Task<IActionResult> OrderGear(int? categoryId, int? sortBy, int? priceRange, string popularity)
        {
            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            string apiUrl = "https://localhost:7298/api/CampingGear/GetCampingGearsBySort?";

            // Thêm các tham số vào URL
            if (categoryId.HasValue)
            {
                apiUrl += $"categoryId={categoryId.Value}&";
            }

            if (sortBy.HasValue)
            {
                apiUrl += $"sortBy={sortBy.Value}&";
            }

            if (priceRange.HasValue)
            {
                apiUrl += $"priceRange={priceRange.Value}&";
            }

            if (!string.IsNullOrEmpty(popularity))
            {
                apiUrl += $"popularity={popularity}";
            }

            var campingGears = await GetDataFromApiAsync<List<GearVM>>(apiUrl);
            var campingCategories = await GetDataFromApiAsync<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");

            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0";
            ViewBag.PriceRange = priceRange.HasValue ? priceRange.Value.ToString() : null; // Cập nhật ViewBag cho PriceRange
            ViewBag.Popularity = popularity; // Cập nhật ViewBag cho Popularity
            ViewBag.CampingGears = campingGears;
            ViewBag.CampingCategories = campingCategories;

            return View();
        }


        public async Task<IActionResult> OrderFoodAndDrink()
        {
            var foodAndDrink = await GetDataFromApiAsync<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            // Lấy danh sách danh mục thiết bị cắm trại
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;  // Thêm danh sách danh mục vào ViewBag

            return View();
        }
        [HttpGet]
        [Route("order-food-drink")]
        public async Task<IActionResult> OrderFoodAndDrink(int? categoryId, int? sortBy, int? priceRange)
        {
            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            string apiUrl = "https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinksBySort?";

            // Thêm các tham số vào URL
            if (categoryId.HasValue)
            {
                apiUrl += $"categoryId={categoryId.Value}&";
            }

            if (sortBy.HasValue)
            {
                apiUrl += $"sortBy={sortBy.Value}&";
            }

            if (priceRange.HasValue)
            {
                apiUrl += $"priceRange={priceRange.Value}&";
            }


            // Gọi API lấy dữ liệu về đồ ăn và thức uống
            var foodAndDrink = await GetDataFromApiAsync<List<FoodAndDrinkVM>>(apiUrl);
            // Gọi API lấy dữ liệu về danh mục đồ ăn và thức uống
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            // Cập nhật ViewBag với các giá trị cần thiết
            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0"; // Sắp xếp
            ViewBag.PriceRange = priceRange.HasValue ? priceRange.Value.ToString() : null; // Khoảng giá
            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;

            return View();
        }
        public IActionResult FoodDetail()
        {
            return View();
        }

        public async Task<IActionResult> OrderTicket()
        {
            var ticket = await GetDataFromApiAsync<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");
            var ticketCategory = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");

            ViewBag.Ticket = ticket;
            ViewBag.TicketCategories = ticketCategory;


            return View();
        }
        [HttpGet]
        [Route("order-ticket")]
        public async Task<IActionResult> OrderTicket(int? categoryId, int? sortBy)
        {
            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            string apiUrl = "https://localhost:7298/api/Ticket/GetTicketsByCategoryAndSort?";

            // Thêm các tham số vào URL
            if (categoryId.HasValue)
            {
                apiUrl += $"categoryId={categoryId.Value}&";
            }

            if (sortBy.HasValue)
            {
                apiUrl += $"sort={sortBy.Value}&";
            }



            // Gọi API lấy dữ liệu về đồ ăn và thức uống
            var ticket = await GetDataFromApiAsync<List<TicketVM>>(apiUrl);
            // Gọi API lấy dữ liệu về danh mục đồ ăn và thức uống
            var ticketCategory = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");

            // Cập nhật ViewBag với các giá trị cần thiết
            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0"; // Sắp xếp          
            ViewBag.Ticket = ticket;
            ViewBag.TicketCategories = ticketCategory;

            return View();
        }
    }
}
