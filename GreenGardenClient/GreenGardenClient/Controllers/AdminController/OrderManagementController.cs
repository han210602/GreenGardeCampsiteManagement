using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

namespace GreenGardenClient.Controllers.AdminController
{
    public class OrderManagementController : Controller
    {
        private HttpClient _httpClient;

        public OrderManagementController()
        {
            _httpClient = new HttpClient(); // Use 'this._httpClient' to initialize the private field
        }
        private T GetDataFromApi<T>(string url)
        {
            var jwtToken = Request.Cookies["JWTToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<T>().Result;
        }
        public IActionResult Index()
        {



            // Thêm JWT token vào header của yêu cầu
            List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrders");
            List<ActivityVM> activities = GetDataFromApi<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities");

            ViewBag.dataorder = orderdata;

            ViewBag.activities = activities;
            return View();


        }
        [HttpPost]
        public IActionResult UpdateActivityOrder(int idorder, int idactivity)
        {
            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                string apiUrl = $"https://localhost:7298/api/OrderManagement/UpdateActivityOrder/{idorder}/{idactivity}";


                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult EnterDeposit(int idorder, double money)
        {
            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Construct the API URL with the parameters
                string apiUrl = $"https://localhost:7298/api/OrderManagement/EnterDeposit/{idorder}/{money}";

                // Send the PUT request
                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult CancelDeposit(int idorder)
        {
            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Construct the API URL with the parameters
                string apiUrl = $"https://localhost:7298/api/OrderManagement/CancelDeposit/{idorder}";

                // Send the PUT request
                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        public IActionResult CreateOrder()
        {
            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();

            return View(orders);


        }
        [HttpPost]
        public IActionResult CreateOrder(string name, DateTime usagedate, string phone)
        {
            var cart = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            cart = new OrderVM()
            {
                CustomerName = name,
                OrderUsageDate = usagedate,
                PhoneCustomer = phone

            };
            TempData["Notification"] = "Lưu thành công!";
            HttpContext.Session.SetObjectAsJson("OrderCart", cart);
            return View("CreateOrder");


        }
        public IActionResult OrderTicket()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();
            List<TicketVM> tickets = GetDataFromApi<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.TicketId == item.TicketId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }
            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
            }
            ViewBag.tickets = tickets;


            return View("OrderTicket");


        }
        public IActionResult TicketCart(List<int> TicketIds, List<string> Name, List<decimal> Prices, List<int> Quantities)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();


            for (int i = 0; i < TicketIds.Count; i++)
            {

                if (Quantities[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.TicketId == TicketIds[i]);
                    if (item != null)
                    {
                        item.Quantity = Quantities[i];
                    }
                    else
                    {
                        cart.Add(new TicketVM() { TicketId = TicketIds[i], TicketName = Name[i], Price = Prices[i], Quantity = Quantities[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.TicketId == TicketIds[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }


            }
            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Hãy tiếp tục đặt đồ cắm trại nào.";
            HttpContext.Session.SetObjectAsJson("TicketCart", cart);
            return RedirectToAction("OrderTicket");
        }
        public IActionResult OrderGear()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();

            List<GearVM> tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.GearId == item.GearId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.gears = tickets;
            return View("OrderGear");
        }
        public IActionResult GearCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();


            for (int i = 0; i < id.Count; i++)
            {

                if (quantity[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.GearId == id[i]);

                    if (item != null)
                    {
                        item.Quantity = quantity[i];
                    }
                    else
                    {
                        cart.Add(new GearVM() { GearId = id[i], GearName = name[i], RentalPrice = price[i], Quantity = quantity[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.GearId == id[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }



            }
            HttpContext.Session.SetObjectAsJson("GearCart", cart);
            return RedirectToAction("OrderGear");
        }
        public IActionResult OrderFood()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();

            List<FoodAndDrinkVM> foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            foreach (var item in ticketscart)
            {
                var ticket = foodAndDrinks.ToList().FirstOrDefault(s => s.ItemId == item.ItemId);
                if (ticket != null)
                {
                    ticket.Description = item.Description;
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.gears = foodAndDrinks;
            return View("OrderFood");
        }
        public IActionResult FoodCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity, List<string> description)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();


            for (int i = 0; i < id.Count; i++)
            {

                if (quantity[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.ItemId == id[i]);

                    if (item != null)
                    {
                        item.Quantity = quantity[i];
                    }
                    else
                    {
                        cart.Add(new FoodAndDrinkVM() { ItemId = id[i], ItemName = name[i], Price = price[i], Description = description[i], Quantity = quantity[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.ItemId == id[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }



            }
            HttpContext.Session.SetObjectAsJson("FoodCart", cart);
            return RedirectToAction("OrderFood");
        }
        public IActionResult OrderComboFood()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<ComboFoodVM>>("ComboFoodCart") ?? new List<ComboFoodVM>();

            List<ComboFoodVM> tickets = GetDataFromApi<List<ComboFoodVM>>("https://localhost:7298/api/ComboFood/GetAllOrders\r\n");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.ComboId == item.ComboId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.gears = tickets;
            return View("OrderComboFood");
        }
        public IActionResult ComboFoodCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ComboFoodVM>>("ComboFoodCart") ?? new List<ComboFoodVM>();


            for (int i = 0; i < id.Count; i++)
            {

                if (quantity[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.ComboId == id[i]);

                    if (item != null)
                    {
                        item.Quantity = quantity[i];
                    }
                    else
                    {
                        cart.Add(new ComboFoodVM() { ComboId = id[i], ComboName = name[i], Price = price[i], Quantity = quantity[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.ComboId == id[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }



            }
            HttpContext.Session.SetObjectAsJson("ComboFoodCart", cart);
            return RedirectToAction("OrderComboFood");
        }
        public IActionResult OrderCombo()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<ComboVM>>("ComboCart") ?? new List<ComboVM>();

            List<ComboVM> tickets = GetDataFromApi<List<ComboVM>>("https://localhost:7298/api/Combo/GetAllCombos\r\n");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.ComboId == item.ComboId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.gears = tickets;
            return View("OrderCombo");
        }
        public IActionResult ComboCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ComboVM>>("ComboCart") ?? new List<ComboVM>();


            for (int i = 0; i < id.Count; i++)
            {

                if (quantity[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.ComboId == id[i]);

                    if (item != null)
                    {
                        item.Quantity = quantity[i];
                    }
                    else
                    {
                        cart.Add(new ComboVM() { ComboId = id[i], ComboName = name[i], Price = price[i], Quantity = quantity[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.ComboId == id[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }



            }
            HttpContext.Session.SetObjectAsJson("ComboCart", cart);
            return RedirectToAction("OrderCombo");
        }
        public IActionResult Cart()
        {
            var tickets = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();
            var gears = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
            var foods = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();
            var combos = HttpContext.Session.GetObjectFromJson<List<ComboVM>>("ComboCart") ?? new List<ComboVM>();
            var combofoods = HttpContext.Session.GetObjectFromJson<List<ComboFoodVM>>("ComboFoodCart") ?? new List<ComboFoodVM>();

            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            decimal total = 0;
            foreach (var item in tickets)
            {
                total += item.Quantity * item.Price;
            }
            foreach (var item in gears)
            {
                total += item.Quantity * item.RentalPrice;
            }
            foreach (var item in foods)
            {
                total += item.Quantity * item.Price;
            }
            foreach (var item in combos)
            {
                total += item.Quantity * item.Price;
            }
            foreach (var item in combofoods)
            {
                total += item.Quantity * item.Price;
            }

            ViewBag.tickets = tickets;
            ViewBag.gears = gears;
            ViewBag.foods = foods;
            ViewBag.order = orders;
            ViewBag.combos = combos;
            ViewBag.combofoods = combofoods;
            ViewBag.Total = total;

            return View("Cart");
        }
        [HttpPost]
        public async Task<IActionResult> Order(decimal deposit, decimal total)
        {
            // Retrieve session data
            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            var tickets = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();
            var gears = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
            var foods = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();
            var combofoods = HttpContext.Session.GetObjectFromJson<List<ComboFoodVM>>("ComboFoodCart") ?? new List<ComboFoodVM>();
            var combos = HttpContext.Session.GetObjectFromJson<List<ComboVM>>("ComboCart") ?? new List<ComboVM>();

            // Retrieve JWT Token from cookies


            // Ensure there's at least one item to process (tickets, gears, etc.)
            if (tickets.Any())
            {
                // Create order request
                var orderRequest = new CreateUniqueOrderRequest
                {
                    Order = new OrderAddDTO
                    {
                        EmployeeId = 2,  // Assuming this is static for now
                        CustomerName = orders.CustomerName,
                        OrderUsageDate = orders.OrderUsageDate,
                        Deposit = deposit,
                        TotalAmount = total,
                        PhoneCustomer = orders.PhoneCustomer,
                    },
                    OrderTicket = tickets.Select(t => new OrderTicketAddlDTO
                    {
                        TicketId = t.TicketId,
                        Quantity = t.Quantity,
                    }).ToList(),
                    OrderCampingGear = gears.Select(g => new OrderCampingGearAddDTO
                    {
                        GearId = g.GearId,
                        Quantity = g.Quantity
                    }).ToList(),
                    OrderFood = foods.Select(f => new OrderFoodAddDTO
                    {
                        ItemId = f.ItemId,
                        Quantity = f.Quantity
                    }).ToList(),
                    OrderFoodCombo = combofoods.Select(cf => new OrderFoodComboAddDTO
                    {
                        ComboId = cf.ComboId,
                        Quantity = cf.Quantity
                    }).ToList()
                };

                try
                {

                    var apiUrl = "https://localhost:7298/api/OrderManagement/CreateUniqueOrder\r\n";

                    // Serialize request object to JSON
                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                    // Make the API call
                    var response = await _httpClient.PostAsync(apiUrl, content);

                    // Process API response
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index");  // Return success message or redirect if needed
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, $"Error: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            else
            {
                var orderRequest = new CreateComboOrderRequest
                {
                    Order = new OrderAddDTO
                    {
                        EmployeeId = 2,  // Assuming this is static for now
                        CustomerName = orders.CustomerName,
                        OrderUsageDate = orders.OrderUsageDate,
                        Deposit = deposit,
                        TotalAmount = total,
                        PhoneCustomer = orders.PhoneCustomer,
                    },
                    OrderCombo = combos.Select(t => new OrderComboAddDTO
                    {
                        ComboId = t.ComboId,
                        Quantity = t.Quantity,
                    }).ToList(),
                    OrderCampingGear = gears.Select(g => new OrderCampingGearAddDTO
                    {
                        GearId = g.GearId,
                        Quantity = g.Quantity
                    }).ToList(),
                    OrderFood = foods.Select(f => new OrderFoodAddDTO
                    {
                        ItemId = f.ItemId,
                        Quantity = f.Quantity
                    }).ToList(),
                    OrderFoodCombo = combofoods.Select(cf => new OrderFoodComboAddDTO
                    {
                        ComboId = cf.ComboId,
                        Quantity = cf.Quantity
                    }).ToList()
                };

                try
                {

                    var apiUrl = "https://localhost:7298/api/OrderManagement/CreateComboOrder";

                    // Serialize request object to JSON
                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                    // Make the API call
                    var response = await _httpClient.PostAsync(apiUrl, content);

                    // Process API response
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, $"Error: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // If no items to process, return the CreateOrder view
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCart()
        {
            // Xóa OrderCart
            HttpContext.Session.Remove("OrderCart");

            // Xóa TicketCart
            HttpContext.Session.Remove("TicketCart");

            // Xóa GearCart
            HttpContext.Session.Remove("GearCart");

            // Xóa FoodCart
            HttpContext.Session.Remove("FoodCart");

            // Xóa ComboFoodCart
            HttpContext.Session.Remove("ComboFoodCart");
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult OrderDetail(int id)
        {
            OrderDetailVM orderdata = GetDataFromApi<OrderDetailVM>($"https://localhost:7298/api/OrderManagement/GetOrderDetail/{id}");
            HttpContext.Session.SetObjectAsJson("order", orderdata.OrderId);
            HttpContext.Session.SetObjectAsJson("TicketUpdateCart", orderdata.OrderTicketDetails);
            HttpContext.Session.SetObjectAsJson("ComboCart", orderdata.OrderComboDetails);
            HttpContext.Session.SetObjectAsJson("ComboFoodCart", orderdata.OrderFoodComboDetails);
            HttpContext.Session.SetObjectAsJson("FoodCart", orderdata.OrderFoodDetails);
            HttpContext.Session.SetObjectAsJson("GearCart", orderdata.OrderCampingGearDetails);




            return View("OrderDetail", orderdata);

        }
        [HttpGet]
        public IActionResult UpdateTicket()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderTicketDetailDTO>>("TicketUpdateCart") ?? new List<OrderTicketDetailDTO>();
            List<TicketVM> tickets = GetDataFromApi<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.TicketId == item.TicketId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity.Value;
                }
            }
            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
            }
            ViewBag.tickets = tickets;
            return View("UpdateTicket");



        }
        [HttpPost]
        public IActionResult UpdateTicket(List<int> TicketIds, List<string> Name, List<decimal> Prices, List<int> Quantities)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();


            for (int i = 0; i < TicketIds.Count; i++)
            {

                if (Quantities[i] > 0)
                {
                    var item = cart.FirstOrDefault(t => t.TicketId == TicketIds[i]);
                    if (item != null)
                    {
                        item.Quantity = Quantities[i];
                    }
                    else
                    {
                        cart.Add(new TicketVM() { TicketId = TicketIds[i], TicketName = Name[i], Price = Prices[i], Quantity = Quantities[i] });

                    }
                }
                else
                {
                    var item = cart.FirstOrDefault(t => t.TicketId == TicketIds[i]);
                    if (item != null)
                    {
                        cart.Remove(item);
                    }

                }


            }
            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Hãy tiếp tục đặt đồ cắm trại nào.";
            HttpContext.Session.SetObjectAsJson("TicketCart", cart);
            return RedirectToAction("OrderTicket");
        }
    }
}
