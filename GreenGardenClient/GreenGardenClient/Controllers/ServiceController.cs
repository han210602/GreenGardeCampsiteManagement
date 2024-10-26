using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

            return View("OrderGear");
        }
        [HttpGet]
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

            return View("OrderGear");
        }


        public async Task<IActionResult> OrderFoodAndDrink()
        {
            var foodAndDrink = await GetDataFromApiAsync<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            // Lấy danh sách danh mục thiết bị cắm trại
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            // Đưa dữ liệu vào ViewBag
            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;  // Thêm danh sách danh mục vào ViewBag

            return View("OrderFoodAndDrink");
        }
        [HttpGet]
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

            return View("OrderFoodAndDrink");
        }
        [HttpGet("FoodDetail")]
        public async Task<IActionResult> FoodDetail(int itemId)
        {
            var apiUrl = $"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinkDetail?itemId={itemId}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var foodAndDrink = JsonConvert.DeserializeObject<FoodAndDrinkVM>(content);

                    if (foodAndDrink != null)
                    {
                        ViewBag.FoodAndDrink = foodAndDrink;
                        return View("FoodDetail", foodAndDrink); // Updated view name
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid data!";
                        return RedirectToAction("Error"); // Redirect to a dedicated error view
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve data from API: {response.StatusCode}";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        [HttpGet("CampingGearDetail")]
        public async Task<IActionResult> CampingGearDetail(int gearId)
        {
            var apiUrl = $"https://localhost:7298/api/CampingGear/GetCampingGearDetail?id={gearId}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var campingGear = JsonConvert.DeserializeObject<GearVM>(content);

                    if (campingGear != null)
                    {
                        ViewBag.CampingGear = campingGear;
                        return View("CampingGearDetail", campingGear); // Updated view name
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid data!";
                        return RedirectToAction("Error"); // Redirect to a dedicated error view
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve data from API: {response.StatusCode}";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
    
        [HttpGet("TicketDetail")]
        public async Task<IActionResult> TicketDetail(int ticketId)
        {
            var apiUrl = $"https://localhost:7298/api/Ticket/GetTicketDetail?id={ticketId}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ticket = JsonConvert.DeserializeObject<TicketVM>(content);

                    if (ticket != null)
                    {
                        ViewBag.Ticket = ticket;
                        return View("TicketDetail", ticket); // Updated view name
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid data!";
                        return RedirectToAction("Error"); // Redirect to a dedicated error view
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve data from API: {response.StatusCode}";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> OrderHistory(int customerId = 1003, bool? statusOrder = null, int? activityId = null)
        {
            ViewBag.CurrentCategoryId = activityId; // Set current category for highlighting
            ViewBag.OrderStatus = statusOrder; // Set current order status for sorting

            // Build API URL dynamically based on filters
            string apiUrl = $"https://localhost:7298/api/OrderManagement/GetCustomerOrders?customerId={customerId}";
            if (activityId.HasValue)
            {
                apiUrl += $"&activityId={activityId.Value}";
            }
            if (statusOrder.HasValue)
            {
                apiUrl += $"&statusOrder={statusOrder.Value.ToString().ToLower()}";
            }

            // Fetch data from APIs
            var order = await GetDataFromApiAsync<List<CustomerOrderVM>>(apiUrl);
            var activity = await GetDataFromApiAsync<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities");

            // Pass data to View
            ViewBag.CustomerOrder = order;
            ViewBag.Activity = activity;
            ViewBag.CustomerId = customerId;

            return View("OrderHistory");
        }


        public async Task<IActionResult> OrderDetailHistory(int orderId)
        {
            var apiUrl = $"https://localhost:7298/api/OrderManagement/GetCustomerOrderDetail/{orderId}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var orderDetail = JsonConvert.DeserializeObject<OrderDetailVM>(content);

                    if (orderDetail != null)
                    {
                        ViewBag.OrderDetail = orderDetail;
                        return View("OrderDetailHistory", orderDetail); // Updated view name
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid data!";
                        return RedirectToAction("Error"); // Redirect to a dedicated error view
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve data from API: {response.StatusCode}";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        public async Task<IActionResult> OrderTicket()
        {
            var ticket = await GetDataFromApiAsync<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");
            var ticketCategory = await GetDataFromApiAsync<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");

            ViewBag.Ticket = ticket;
            ViewBag.TicketCategories = ticketCategory;


            return View("OrderTicket");
        }
        [HttpGet]
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

            return View("OrderTicket");
        }

        public IActionResult Cart()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        private List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session.GetString("Cart");
            if (session == null)
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(session);
        }

        // Cập nhật giỏ hàng vào Session
        private void SaveCartItems(List<CartItem> cartItems)
        {
            var session = JsonConvert.SerializeObject(cartItems);
            HttpContext.Session.SetString("Cart", session);
        }
        [HttpPost]
        public IActionResult AddToCart(int Id, string Name, string CategoryName, string Type, string TypeCategory, decimal price, int quantity, string usageDate, string redirectAction)
        {
            var cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(c => c.Id == Id && c.Type == Type && c.TypeCategory == TypeCategory);

            // Chuyển đổi từ chuỗi ngày sang DateTime
            DateTime parsedDate;
            if (!DateTime.TryParseExact(usageDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                parsedDate = DateTime.Now; // Hoặc giá trị mặc định nếu không parse được
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    Id = Id,
                    Name = Name,
                    CategoryName = CategoryName,
                    Type = Type,
                    TypeCategory = TypeCategory,
                    Price = price,
                    Quantity = quantity,
                    UsageDate = parsedDate // Gán ngày sử dụng đã được chuyển đổi
                };
                cartItems.Add(newItem);
            }

            SaveCartItems(cartItems);
            if (redirectAction == "FoodDetail")
            {
                return RedirectToAction(redirectAction, new { itemId = Id });
            }
            else if (redirectAction == "CampingGearDetail")
            {
                return RedirectToAction(redirectAction, new { gearId = Id });
            }
            else if (redirectAction == "TicketDetail")
            {
                return RedirectToAction(redirectAction, new { ticketId = Id });
            }
            else
            {
                return RedirectToAction(redirectAction);
            }

        }

        [HttpPost]
        public IActionResult UpdateUsageDate(string usageDate, int ticketId)
        {
            // Tìm vé theo ticketId và cập nhật ngày mới
            var cartItems = GetCartItems();
            var ticket = cartItems.FirstOrDefault(t => t.Id == ticketId);
            if (ticket != null)
            {
                // Parse lại ngày sử dụng và gán cho vé
                if (DateTime.TryParse(usageDate, out DateTime parsedDate))
                {
                    ticket.UsageDate = parsedDate; // Cập nhật ngày sử dụng mới
                }
            }

            SaveCartItems(cartItems);

            // Trả về view cập nhật lại thông tin vé bên OrderTicket
            return RedirectToAction("Cart");
        }



        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public IActionResult RemoveFromCart(int ticketId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(c => c.Id == ticketId);
            if (item != null)
            {
                cartItems.Remove(item);
            }

            SaveCartItems(cartItems);
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public IActionResult UpdateCartItemQuantity(int ticketId, int newQuantity)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(c => c.Id == ticketId);

            if (item != null)
            {
                item.Quantity = newQuantity; // Cập nhật số lượng
            }

            SaveCartItems(cartItems);
            return Ok(); // Trả về kết quả thành công
        }


        public IActionResult UpdateProfile()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Order()
        {
            // Retrieve cart items from session
            var cartItems = GetCartItems();

            // Ensure there's at least one item in the cart
            if (!cartItems.Any())
            {
                TempData["Notification"] = "Giỏ hàng của bạn trống!";
                return RedirectToAction("Cart");
            }

            try
            {
                // Create a new HttpClient instance from the factory
                var client = _clientFactory.CreateClient();

                // Classify cart items into different categories
                var tickets = cartItems.Where(c => c.Type == "Ticket" && c.TypeCategory == "TicketCategory").ToList();
                var gears = cartItems.Where(c => c.Type == "Gear" && c.TypeCategory == "GearCategory").ToList();
                var foods = cartItems.Where(c => c.Type == "FoodAndDrink" && c.TypeCategory == "FoodAndDrinkCategory").ToList();
                var combofoods = cartItems.Where(c => c.TypeCategory == "Combo").ToList();

                // Assuming all cart items share the same usage date
                var usageDate = cartItems.First().UsageDate;

                // Calculate total amount
                var totalAmount = cartItems.Sum(item => item.TotalPrice);

                // Create the order request
                var orderRequest = new CheckOut
                {
                    Order = new CustomerOrderAddDTO
                    {
                        CustomerId = 1003,  // Use actual customer ID
                        CustomerName = "Test Customer",  // Replace with actual customer data
                        OrderDate = DateTime.Now,
                        OrderUsageDate = usageDate,
                        Deposit = 0,
                        TotalAmount = totalAmount,
                        PhoneCustomer = "1234567890"  // Replace with actual phone number
                    },
                    OrderTicket = tickets.Select(t => new CustomerOrderTicketAddlDTO
                    {
                        TicketId = t.Id,
                        Quantity = t.Quantity
                    }).ToList(),
                    OrderCampingGear = gears.Select(g => new CustomerOrderCampingGearAddDTO
                    {
                        GearId = g.Id,
                        Quantity = g.Quantity
                    }).ToList(),
                    OrderFood = foods.Select(f => new CustomerOrderFoodAddDTO
                    {
                        ItemId = f.Id,
                        Quantity = f.Quantity,
                        Description = f.Name
                    }).ToList(),
                    OrderFoodCombo = combofoods.Select(cf => new CustomerOrderFoodComboAddDTO
                    {
                        ComboId = cf.Id,
                        Quantity = cf.Quantity
                    }).ToList()
                };

                // Serialize the order request to JSON
                var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                // Use the client from IHttpClientFactory to make the API call
                var apiUrl = "https://localhost:7298/api/OrderManagement/CheckOut";
                var response = await client.PostAsync(apiUrl, content);

                // Handle the API response
                if (response.IsSuccessStatusCode)
                {
                    // Clear the cart after successful checkout
                    HttpContext.Session.Remove("Cart");  // This clears the cart items in the session

                    TempData["Notification"] = "Đặt hàng thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["Notification"] = $"Lỗi khi đặt hàng: {errorMessage}";
                    return RedirectToAction("Cart");
                }
            }
            catch (Exception ex)
            {
                TempData["Notification"] = $"Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("Cart");
            }
        }

    }
}
