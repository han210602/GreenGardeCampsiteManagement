using GreenGardenClient.Hubs;
using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHttpClientFactory _clientFactory;// Đảm bảo bạn đã khai báo IHttpClientFactory
        private readonly IHubContext<CartHub> _hubContext;

        public ServiceController(ILogger<ServiceController> logger, IHttpClientFactory clientFactory, IHubContext<CartHub> hubContext)
        {
            _hubContext = hubContext;
            _logger = logger;
            _clientFactory = clientFactory;// Khởi tạo IHttpClientFactory
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
            var comboFood = await GetDataFromApiAsync<List<ComboFoodVM>>("https://localhost:7298/api/ComboFood/GetAllOrders");
            Console.WriteLine("-----------"+comboFood.Count);
            // Lấy danh sách danh mục thiết bị cắm trại
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");
            foreach(var item in comboFood)
            {
                foodAndDrink.Add(new FoodAndDrinkVM()
                {
                    ItemId = item.ComboId,
                    ItemName = item.ComboName,
                    Price = item.Price,
                    ImgUrl=item.ImgUrl,
                    CategoryName="combo"
                });
            }

            ViewBag.FoodAndDrink = foodAndDrink;
            ViewBag.FoodAndDrinkCategories = foodAndDrinkCategories;  // Thêm danh sách danh mục vào ViewBag

            return View("OrderFoodAndDrink");
        }
        [HttpGet]
        public async Task<IActionResult> OrderFoodAndDrink(int? categoryId, int? sortBy, int? priceRange)
        {
            var foodAndDrinkCategories = await GetDataFromApiAsync<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories");

            ViewBag.CurrentCategoryId = categoryId; // Lưu lại categoryId hiện tại để hiển thị active

            // Xây dựng URL cho API
            if (categoryId == 9999)
            {
                var foodAndDrink=new List<FoodAndDrinkVM>();
                var comboFood = await GetDataFromApiAsync<List<ComboFoodVM>>("https://localhost:7298/api/ComboFood/GetAllOrders");

                foreach (var item in comboFood)
                {
                    foodAndDrink.Add(new FoodAndDrinkVM()
                    {
                        ItemId = item.ComboId,
                        ItemName = item.ComboName,
                        Price = item.Price,
                        ImgUrl = item.ImgUrl,
                        CategoryName = "combo"
                    });
                }

            }
            else
            {
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

                // Cập nhật ViewBag với các giá trị cần thiết
             
                ViewBag.FoodAndDrink = foodAndDrink;
            }
            ViewBag.SortBy = sortBy.HasValue ? sortBy.Value.ToString() : "0"; // Sắp xếp
            ViewBag.PriceRange = priceRange.HasValue ? priceRange.Value.ToString() : null; // Khoảng giá
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
        public async Task<IActionResult> OrderHistory(bool? statusOrder = null, int? activityId = null)
        {
            // Retrieve the CustomerId from the session
            var customerId = HttpContext.Session.GetInt32("UserId");

            // Ensure customerId is found
            if (!customerId.HasValue)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem lịch sử đặt hàng!";
                return RedirectToAction("Login");
            }

            ViewBag.CurrentCategoryId = activityId; // Set current category for highlighting
            ViewBag.OrderStatus = statusOrder; // Set current order status for sorting

            // Build API URL dynamically based on filters
            string apiUrl = $"https://localhost:7298/api/OrderManagement/GetCustomerOrders?customerId={customerId.Value}";
            if (activityId.HasValue)
            {
                apiUrl += $"&activityId={activityId.Value}";
            }
            if (statusOrder.HasValue)
            {
                apiUrl += $"&statusOrder={statusOrder.Value.ToString().ToLower()}";
            }

            try
            {
                var client = _clientFactory.CreateClient();

                // Retrieve JWT token from cookies
                var jwtToken = Request.Cookies["JWTToken"];
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy JWT token!";
                    return RedirectToAction("Error");
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
            catch (HttpRequestException httpEx)
            {
                TempData["ErrorMessage"] = $"Request error: {httpEx.Message}";
                return RedirectToAction("Error");
            }
            catch (JsonException jsonEx)
            {
                TempData["ErrorMessage"] = $"Error processing data: {jsonEx.Message}";
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        public async Task<IActionResult> OrderDetailHistory(int orderId)
        {
            var apiUrl = $"https://localhost:7298/api/OrderManagement/GetCustomerOrderDetail/{orderId}";

            try
            {
                var client = _clientFactory.CreateClient();

                // Retrieve JWT token from cookies
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

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
            catch (HttpRequestException httpEx)
            {
                TempData["ErrorMessage"] = $"Request error: {httpEx.Message}";
                return RedirectToAction("Error");
            }
            catch (JsonException jsonEx)
            {
                TempData["ErrorMessage"] = $"Error processing data: {jsonEx.Message}";
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"System error: {ex.Message}";
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var apiUrl = $"https://localhost:7298/api/OrderManagement/ChangeCustomerActivity?orderId={orderId}";

            try
            {
                var client = _clientFactory.CreateClient();

                // Lấy JWT token từ cookies và thêm vào header
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                // Gửi yêu cầu hủy đơn hàng
                var response = await client.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Hủy đơn hàng thành công!" });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"Lỗi khi hủy đơn hàng: {errorMessage}" });
                }
            }
            catch (HttpRequestException httpEx)
            {
                return Json(new { success = false, message = $"Lỗi khi gửi yêu cầu: {httpEx.Message}" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
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
        public async Task<IActionResult> AddToCartAsync(int Id, string Name, string CategoryName, string Type, string TypeCategory, decimal price, int quantity, string usageDate, string redirectAction)
        {
            // Kiểm tra người dùng đã đăng nhập chưa
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("RoleId");

            if (userId == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để thêm vào giỏ hàng!" });
            }

            if (userRole != 3)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập với quyền khách hàng để thêm vào giỏ hàng!" });
            }

            // Lấy danh sách sản phẩm trong giỏ hàng từ session
            var cartItems = GetCartItems();

            // Kiểm tra nếu giỏ hàng chứa vé hoặc combo
            bool hasTicket = cartItems.Any(c => c.Type == "Ticket" && c.TypeCategory == "TicketCategory");
            bool hasCombo = cartItems.Any(c => c.Type == "Combo" && c.TypeCategory == "ComboCategory" || c.Id != Id);

            // Nếu đang thêm một combo
            if (Type == "Combo")
            {
                // Kiểm tra nếu đã có một combo khác hoặc có vé trong giỏ hàng
                if (hasCombo)
                {
                    return Json(new { success = false, message = "Giỏ hàng chỉ cho phép một loại combo duy nhất." });
                }

                if (hasTicket)
                {
                    return Json(new { success = false, message = "Không thể thêm combo vào giỏ hàng vì đã có vé trong giỏ hàng." });
                }
            }

            // Nếu đang thêm một vé
            if (Type == "Ticket")
            {
                // Kiểm tra nếu đã có combo trong giỏ hàng
                if (hasCombo)
                {
                    return Json(new { success = false, message = "Không thể thêm vé vào giỏ hàng vì đã có combo trong giỏ hàng." });
                }
            }

            // Chuyển đổi usageDate từ chuỗi sang DateTime
            DateTime parsedDateTime;
            if (!DateTime.TryParseExact(usageDate, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
            {
                parsedDateTime = DateTime.Now; // Giá trị mặc định nếu chuyển đổi thất bại
            }

            // Kiểm tra nếu sản phẩm đã tồn tại trong giỏ hàng
            var existingItem = cartItems.FirstOrDefault(c => c.Id == Id && c.Type == Type && c.TypeCategory == TypeCategory);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // Cập nhật số lượng nếu sản phẩm đã tồn tại
            }
            else
            {
                // Thêm sản phẩm mới vào giỏ hàng
                var newItem = new CartItem
                {
                    Id = Id,
                    Name = Name,
                    CategoryName = CategoryName,
                    Type = Type,
                    TypeCategory = TypeCategory,
                    Price = price,
                    Quantity = quantity,
                    UsageDate = parsedDateTime
                };
                cartItems.Add(newItem);
            }

            await _hubContext.Clients.All.SendAsync("ReceiveCartUpdate", cartItems);

            // Lưu giỏ hàng cập nhật vào session
            SaveCartItems(cartItems);

            // Trả về JSON cho AJAX với thông báo thành công
            return Json(new { success = true, message = "Thêm vào giỏ hàng thành công!", cartItemCount = cartItems.Count });
        }





        [HttpPost]
        public IActionResult UpdateCartUsageDate(string usageDate)
        {
            var cartItems = GetCartItems(); // Lấy danh sách giỏ hàng từ session

            // Chuyển đổi ngày sử dụng thành DateTime
            if (DateTime.TryParseExact(usageDate, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                // Cập nhật ngày sử dụng cho từng mặt hàng trong giỏ hàng
                foreach (var item in cartItems)
                {
                    item.UsageDate = parsedDateTime; // Cập nhật ngày sử dụng
                }

                SaveCartItems(cartItems); // Lưu giỏ hàng vào session
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Ngày sử dụng không hợp lệ." });
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
        public IActionResult UpdateCartItemQuantity(int ticketId, int newQuantity, string type, string typeCategory)
        {
            // Retrieve the cart items
            var cartItems = GetCartItems();

            // Find the specific item in the cart based on the composite key
            var item = cartItems.FirstOrDefault(c => c.Id == ticketId && c.Type == type && c.TypeCategory == typeCategory);

            if (item != null)
            {
                // Update the quantity
                item.Quantity = newQuantity;
            }
            else
            {
                // If no item matches, consider adding it as a new entry if desired
            }

            // Save the updated cart items
            SaveCartItems(cartItems);

            return Ok();
        }


        public async Task<IActionResult> ComboList()
        {
            var combo = await GetDataFromApiAsync<List<ComboVM>>("https://localhost:7298/api/Combo/GetAllCombos");

            ViewBag.Combo = combo;


            return View("ComboList");
        }
        [HttpGet("ComboDetail")]
        public async Task<IActionResult> ComboDetail(int comboId)
        {
            var apiUrl = $"https://localhost:7298/api/Combo/GetComboDetail/{comboId}";

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var combo = JsonConvert.DeserializeObject<ComboDetailVM>(content);

                    if (combo != null)
                    {
                        ViewBag.Combo = combo;
                        return View("ComboDetail", combo); // Updated view name
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
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order()
        {
            // Check if the user is logged in
            var customerId = HttpContext.Session.GetInt32("UserId");
            if (customerId == null)
            {
                TempData["Notification"] = "Vui lòng đăng nhập để thực hiện đặt hàng!";
                return RedirectToAction("Login"); // Redirect to login if not logged in
            }

            // Retrieve cart items from session
            var cartItems = GetCartItems();

            // Ensure there's at least one item in the cart
            if (!cartItems.Any())
            {
                TempData["Notification"] = "Giỏ hàng của bạn trống!";
                return RedirectToAction("Cart");
            }

            // Check for tickets or combos in the cart
            var tickets = cartItems.Where(c => c.Type == "Ticket" && c.TypeCategory == "TicketCategory").ToList();
            var combos = cartItems.Where(c => c.Type == "Combo" && c.TypeCategory == "ComboCategory").ToList();

            // If there is no ticket and no combo, notify the user and redirect to the cart
            if (!tickets.Any() && !combos.Any())
            {
                TempData["Notification"] = "Giỏ hàng của bạn cần có ít nhất một vé hoặc combo để đặt hàng!";
                return RedirectToAction("Cart");
            }

            try
            {
                // Create a new HttpClient instance from the factory
                var client = _clientFactory.CreateClient();
                var jwtToken = Request.Cookies["JWTToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Classify other cart items
                var gears = cartItems.Where(c => c.Type == "Gear" && c.TypeCategory == "GearCategory").ToList();
                var foods = cartItems.Where(c => c.Type == "FoodAndDrink" && c.TypeCategory == "FoodAndDrinkCategory").ToList();
                var comboFoods = cartItems.Where(c => c.TypeCategory == "Combo").ToList();

                // Assuming all cart items share the same usage date
                var usageDate = cartItems.First().UsageDate;

                // Calculate total amount
                var totalAmount = cartItems.Sum(item => item.TotalPrice);

                // Prepare order request based on the types of items in the cart
                if (tickets.Any())
                {
                    var orderRequest = new CheckOut
                    {
                        Order = new CustomerOrderAddDTO
                        {
                            CustomerId = customerId.Value,
                            CustomerName = HttpContext.Session.GetString("Fullname"),
                            OrderDate = DateTime.Now,
                            OrderUsageDate = usageDate,
                            Deposit = 0,
                            TotalAmount = totalAmount,
                            PhoneCustomer = HttpContext.Session.GetString("NumberPhone")
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
                        OrderFoodCombo = comboFoods.Select(cf => new CustomerOrderFoodComboAddDTO
                        {
                            ComboId = cf.Id,
                            Quantity = cf.Quantity
                        }).ToList()
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                    // Use the client from IHttpClientFactory to make the API call
                    var apiUrl = "https://localhost:7298/api/OrderManagement/CheckOut";
                    var response = await client.PostAsync(apiUrl, content);

                    // Handle the API response
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.Remove("Cart");
                        TempData["Notification"] = "Đặt hàng thành công!";
                        return RedirectToAction("OrderHistory");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        TempData["Notification"] = $"Lỗi khi đặt hàng: {errorMessage}";
                        return RedirectToAction("Cart");
                    }
                }
                else
                {
                    var orderRequest = new CheckOutComboOrderRequest
                    {
                        Order = new CustomerOrderAddDTO
                        {
                            CustomerId = customerId.Value,
                            CustomerName = HttpContext.Session.GetString("Fullname"),
                            OrderDate = DateTime.Now,
                            OrderUsageDate = usageDate,
                            Deposit = 0,
                            TotalAmount = totalAmount,
                            PhoneCustomer = HttpContext.Session.GetString("NumberPhone")
                        },
                        OrderCombo = combos.Select(t => new CustomerOrderComboAddDTO
                        {
                            ComboId = t.Id,
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
                        OrderFoodCombo = comboFoods.Select(cf => new CustomerOrderFoodComboAddDTO
                        {
                            ComboId = cf.Id,
                            Quantity = cf.Quantity
                        }).ToList()
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                    // Use the client from IHttpClientFactory to make the API call
                    var apiUrl = "https://localhost:7298/api/OrderManagement/CheckOutComboOrder";
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.Remove("Cart");
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
            }
            catch (Exception ex)
            {
                TempData["Notification"] = "Xảy ra lỗi ngoài luồng khi tạo đơn.";
                return RedirectToAction("Error");
            }
        }



        //public async Task<IActionResult> Order()
        //{
        //    // Retrieve cart items from session
        //    var cartItems = GetCartItems();

        //    // Ensure there's at least one item in the cart
        //    if (!cartItems.Any())
        //    {
        //        TempData["Notification"] = "Giỏ hàng của bạn trống!";
        //        return RedirectToAction("Cart");
        //    }

        //    try
        //    {
        //        // Create a new HttpClient instance from the factory
        //        var client = _clientFactory.CreateClient();

        //        // Retrieve JWT token from cookies
        //        var jwtToken = Request.Cookies["JWTToken"];
        //        if (!string.IsNullOrEmpty(jwtToken))
        //        {
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        //        }
        //        else
        //        {
        //            TempData["Notification"] = "Vui lòng đăng nhập để thực hiện đặt hàng!";
        //            return RedirectToAction("Login"); // Redirect to login if token is not available
        //        }

        //        // Retrieve the user ID and other customer details from session
        //        var customerId = 4;
        //        var customerName = HttpContext.Session.GetString("FullName");
        //        var phoneCustomer = HttpContext.Session.GetString("NumberPhone");


        //        // Check if the user is authenticated
        //        if (customerId == null || string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phoneCustomer))
        //        {
        //            TempData["Notification"] = "Vui lòng đăng nhập trước khi đặt hàng!";
        //            return RedirectToAction("Login"); // Redirect to login if the user is not authenticated
        //        }

        //        // Classify cart items into different categories
        //        var tickets = cartItems.Where(c => c.Type == "Ticket" && c.TypeCategory == "TicketCategory").ToList();
        //        var gears = cartItems.Where(c => c.Type == "Gear" && c.TypeCategory == "GearCategory").ToList();
        //        var combo = cartItems.Where(c => c.Type == "Combo" && c.TypeCategory == "ComboCategory").ToList();
        //        var foods = cartItems.Where(c => c.Type == "FoodAndDrink" && c.TypeCategory == "FoodAndDrinkCategory").ToList();
        //        var combofoods = cartItems.Where(c => c.TypeCategory == "Combo").ToList();

        //        // Assuming all cart items share the same usage date
        //        var usageDate = cartItems.First().UsageDate;

        //        // Calculate total amount
        //        var totalAmount = cartItems.Sum(item => item.TotalPrice);
        //        if (tickets.Any())
        //        {
        //            var orderRequest = new CheckOut
        //            {
        //                Order = new CustomerOrderAddDTO
        //                {
        //                    CustomerId = customerId, // Use the retrieved customer ID
        //                    CustomerName = customerName, // Retrieve the customer's name from session
        //                    OrderDate = DateTime.Now,
        //                    OrderUsageDate = usageDate,
        //                    Deposit = 0,
        //                    TotalAmount = totalAmount,
        //                    PhoneCustomer = phoneCustomer // Retrieve the customer's phone from session
        //                },
        //                OrderTicket = tickets.Select(t => new CustomerOrderTicketAddlDTO
        //                {
        //                    TicketId = t.Id,
        //                    Quantity = t.Quantity
        //                }).ToList(),
        //                OrderCampingGear = gears.Select(g => new CustomerOrderCampingGearAddDTO
        //                {
        //                    GearId = g.Id,
        //                    Quantity = g.Quantity
        //                }).ToList(),
        //                OrderFood = foods.Select(f => new CustomerOrderFoodAddDTO
        //                {
        //                    ItemId = f.Id,
        //                    Quantity = f.Quantity,
        //                    Description = f.Name
        //                }).ToList(),
        //                OrderFoodCombo = combofoods.Select(cf => new CustomerOrderFoodComboAddDTO
        //                {
        //                    ComboId = cf.Id,
        //                    Quantity = cf.Quantity
        //                }).ToList()
        //            };

        //            // Serialize the order request to JSON
        //            var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

        //            // Use the client from IHttpClientFactory to make the API call
        //            var apiUrl = "https://localhost:7298/api/OrderManagement/CheckOut";
        //            var response = await client.PostAsync(apiUrl, content);

        //            // Handle the API response
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Clear the cart after successful checkout
        //                HttpContext.Session.Remove("Cart");  // This clears the cart items in the session

        //                TempData["Notification"] = "Đặt hàng thành công!";
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                var errorMessage = await response.Content.ReadAsStringAsync();
        //                TempData["Notification"] = $"Lỗi khi đặt hàng: {errorMessage}";
        //                return RedirectToAction("Cart");
        //            }
        //        }
        //        else
        //        {
        //            var orderRequest = new CheckOutComboOrderRequest
        //            {
        //                Order = new CustomerOrderAddDTO
        //                {
        //                    CustomerId = customerId, // Use the retrieved customer ID
        //                    CustomerName = customerName, // Retrieve the customer's name from session
        //                    OrderDate = DateTime.Now,
        //                    OrderUsageDate = usageDate,
        //                    Deposit = 0,
        //                    TotalAmount = totalAmount,
        //                    PhoneCustomer = phoneCustomer // Retrieve the customer's phone from session
        //                },
        //                OrderCombo = tickets.Select(t => new CustomerOrderComboAddDTO
        //                {
        //                    ComboId = t.Id,
        //                    Quantity = t.Quantity
        //                }).ToList(),
        //                OrderCampingGear = gears.Select(g => new CustomerOrderCampingGearAddDTO
        //                {
        //                    GearId = g.Id,
        //                    Quantity = g.Quantity
        //                }).ToList(),
        //                OrderFood = foods.Select(f => new CustomerOrderFoodAddDTO
        //                {
        //                    ItemId = f.Id,
        //                    Quantity = f.Quantity,
        //                    Description = f.Name
        //                }).ToList(),
        //                OrderFoodCombo = combofoods.Select(cf => new CustomerOrderFoodComboAddDTO
        //                {
        //                    ComboId = cf.Id,
        //                    Quantity = cf.Quantity
        //                }).ToList()
        //            };

        //            // Serialize the order request to JSON
        //            var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

        //            // Use the client from IHttpClientFactory to make the API call
        //            var apiUrl = "https://localhost:7298/api/OrderManagement/CheckOutComboOrder";
        //            var response = await client.PostAsync(apiUrl, content);

        //            // Handle the API response
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Clear the cart after successful checkout
        //                HttpContext.Session.Remove("Cart");  // This clears the cart items in the session

        //                TempData["Notification"] = "Đặt hàng thành công!";
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                var errorMessage = await response.Content.ReadAsStringAsync();
        //                TempData["Notification"] = $"Lỗi khi đặt hàng: {errorMessage}";
        //                return RedirectToAction("Cart");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Notification"] = $"Lỗi hệ thống: {ex.Message}";
        //        return RedirectToAction("Cart");
        //    }
        //}


    }
}