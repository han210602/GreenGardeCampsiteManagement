﻿using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            
            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsync<T>().Result;
        }


        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return View();
        }
        public IActionResult Index4()
        {
            return View();
        }
        public IActionResult Index()
        {



            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrders");
                List<ActivityVM> activities = GetDataFromApi<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities");

                ViewBag.dataorder = orderdata;

                ViewBag.activities = activities;
                if (TempData["Notification"] != null)
                {
                    ViewBag.Notification = TempData["Notification"];
                }
                return View();
            }
            catch(Exception e)
            {
                return RedirectToAction("Error");
            }


        }
        public IActionResult Error()
        {
            return View("Error");
        
        }
            public IActionResult OrderOnline()
            {
            try
            {

                String Notification = "";
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                List<OrderVM> orderdata = new List<OrderVM>();
                List<ActivityVM> activities = new List<ActivityVM>();
                if ( HttpContext.Session.GetInt32("RoleId").Value==2)
                {
                    orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrderOnline").Where(s=>s.StatusOrder==true).ToList();
                    activities = GetDataFromApi<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities").Where(s => s.ActivityId == 1 || s.ActivityId == 2||s.ActivityId==1002).ToList();
                
                }
                else
                {
                    orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrderOnline").Where(s=>s.ActivityId==1).ToList();
                     activities = GetDataFromApi<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities").Where(s => s.ActivityId == 1 || s.ActivityId == 2|| s.ActivityId == 1002).ToList();
                    foreach (var item in orderdata)
                    {
                        if (item.OrderUsageDate <= DateTime.Now && item.StatusOrder == false)
                        {
                            
                            string apiUrl = $"https://localhost:7298/api/OrderManagement/UpdateActivityOrder/{item.OrderId}/{1002}";
                            HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;
                            item.ActivityId = 1002;
                            Notification += $"Đơn {item.OrderId} đã bị hủy do tới ngày đặt trước nhưng chưa cọc.\n";
                            orderdata.Remove(item);
                        }
                        if (item.OrderUsageDate < DateTime.Now && item.StatusOrder == true)
                        {
                            string apiUrl = $"https://localhost:7298/api/OrderManagement/UpdateActivityOrder/{item.OrderId}/{1002}";
                            HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;
                            item.ActivityId = 1002;
                            Notification += $"Đơn {item.OrderId} đã bị hủy do quá ngày sử dụng.\n";
                            orderdata.Remove(item);
                        }
                    }
                   

                    
                }
                ViewBag.dataorder = orderdata;

                ViewBag.activities = activities;
                ViewBag.Notification = Notification;
                if (TempData["Notification"] != null)
                {
                    ViewBag.Notification = TempData["Notification"];
                }
                return View("OrderOnline");

            }
            catch
            {
                return RedirectToAction("Error");

            }


        }
        public IActionResult OrderUsing()
        {
            try
            {
                String Notification = "";


                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrders");
                orderdata = orderdata.Where(s => s.ActivityId == 2).ToList();
                List<ActivityVM> activities = GetDataFromApi<List<ActivityVM>>("https://localhost:7298/api/Activity/GetAllActivities");
                activities = activities.Where(s => s.ActivityId == 2 || s.ActivityId == 3).ToList();

                ViewBag.dataorder = orderdata;

                ViewBag.activities = activities;
                ViewBag.Notification = Notification;
                if (TempData["Notification"] != null)
                {
                    ViewBag.Notification = TempData["Notification"];
                }

                return View("OrderUsing");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");

            }


        }
        public IActionResult OrderCheckout()
        {
            try
            {
                String Notification = "";


                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrders");
                orderdata = orderdata.Where(s => s.ActivityId == 3).ToList();
                

                ViewBag.dataorder = orderdata;
                ViewBag.Notification = Notification;
                if (TempData["Notification"] != null)
                {
                    ViewBag.Notification = TempData["Notification"];
                }

                return View("OrderCheckOut");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");

            }


        }
        public IActionResult OrderCancel()
        {
            try
            {
                String Notification = "";


                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/OrderManagement/GetAllOrders");
                orderdata = orderdata.Where(s => s.ActivityId == 1002).ToList();
           

                ViewBag.dataorder = orderdata;

                ViewBag.Notification = Notification;
                if (TempData["Notification"] != null)
                {
                    ViewBag.Notification = TempData["Notification"];
                }

                return View("OrderCancel");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");

            }

        }


        [HttpPost]
        public IActionResult UpdateActivityOrder(int idorder, int idactivity)
        {
            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                string apiUrl = $"https://localhost:7298/api/OrderManagement/UpdateActivityOrder/{idorder}/{idactivity}";

                TempData["Notification"] = "Cập nhập trạng thái đơn thành công!";
                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;
                if (idactivity == 1)
                {
                    TempData["Notification"] = $"Đơn #{idorder} đã chuyển sang trạng thái đợi.";

                    return RedirectToAction("OrderOnline");

                }else if (idactivity == 2)
                {
                    TempData["Notification"] = $"Đơn #{idorder} đã chuyển sang trạng đang sử dụng.";

                    return RedirectToAction("OrderUsing");

                }
                else if(idactivity==3)
                {
                    TempData["Notification"] = $"Đơn #{idorder} đã thanh toán thành công.";

                    return RedirectToAction("OrderCheckOut");

                }
                else
                {
                    TempData["Notification"] = $"Đơn #{idorder} đã bị hủy.";

                    return RedirectToAction("OrderCancel");

                }

            }
            catch (Exception ex)
            {
                TempData["Notification"] = "Cập nhập trạng thái đơn thất bại!";

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
                
                string apiUrl = $"https://localhost:7298/api/OrderManagement/EnterDeposit/{idorder}/{money}";
                TempData["Notification"] = "Đặt cọc thành công!";

                // Send the PUT request
                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;

            }
            catch (Exception ex)
            {
                TempData["Notification"] = "Xảy ra lỗi trong quá trình cọc!";

            }
            return RedirectToAction("OrderOnline");

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
                TempData["Notification"] = "Hủy đặt cọc thành công!";

                // Send the PUT request
                HttpResponseMessage response = _httpClient.PutAsync(apiUrl, null).Result;

            }
            catch (Exception ex)
            {
                TempData["Notification"] = "Xảy ra lỗi trong quá trình hủy cọc!";

            }
            return RedirectToAction("OrderOnline");

        }
        public IActionResult CreateOrder()
        {
            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            if (orders.OrderUsageDate != null)
            {
                string date = orders.OrderUsageDate.Value.ToString("yyyy-MM-ddTHH:mm");
                ViewBag.date = date;

            }
            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
            }
            return View(orders);


        }
        [HttpPost]
        public IActionResult CreateOrder(string name, DateTime usagedate, string phone)
        {
            var cart = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM()
            {
                CustomerName = name,
                OrderUsageDate = usagedate,
                PhoneCustomer = phone
            };

            TempData["Notification"] = "Lưu thành công thông tin khách hàng!";
            HttpContext.Session.SetObjectAsJson("OrderCart", cart);
            return RedirectToAction("CreateOrder");


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
            try
            {
                var ticketscart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
                var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
                if (orders.OrderUsageDate == null)
                {
                    TempData["Notification"] = "Muốn thuê đồ phải đặt ngày";

                    return RedirectToAction("CreateOrder");
                }
                else
                {
                    string formattedDate = orders.OrderUsageDate.Value.ToString("yyyy-MM-dd");

                    List<GearVM> tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");
                    var jwtToken = Request.Cookies["JWTToken"];
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    List<OrderCampingGearByUsageDateDTO> ordergear = GetDataFromApi<List<OrderCampingGearByUsageDateDTO>>($"https://localhost:7298/api/OrderManagement/GetListOrderGearByUsageDate/{formattedDate}");
                    foreach (var item in tickets)
                    {
                        var ticket = ordergear.ToList().Where(s => s.GearId == item.GearId);
                        if (ticket != null)
                        {
                            foreach (var item1 in ticket)
                            {
                                item.QuantityAvailable = item.QuantityAvailable - item1.Quantity.Value;
                            }
                        }
                    }

                    foreach (var item in ticketscart)
                    {
                        var ticket = tickets.ToList().FirstOrDefault(s => s.GearId == item.GearId);
                        if (ticket != null)
                        {
                            ticket.Quantity = item.Quantity;
                            ticket.QuantityAvailable -= item.Quantity;
                        }
                    }

                    if (TempData["Notification"] != null)
                    {
                        ViewBag.Notification = TempData["Notification"];
                    }
                    ViewBag.gears = tickets;
                    return View("OrderGear");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");
            }

        }
        public IActionResult GearCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity, List<int>quantityavai)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();


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
                        cart.Add(new GearVM() { GearId = id[i], GearName = name[i],RentalPrice = price[i], Quantity = quantity[i], QuantityAvailable = quantityavai[i] });

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
            TempData["Notification"] = "Thêm vào giỏ hàng thành công. Hãy tiếp tục đặt đồ ăn nào";

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

            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
            }
            ViewBag.gears = foodAndDrinks;
            return View("OrderFood");
        }
        public IActionResult FoodDetail(int id, int quantity)
        {

            FoodAndDrinkVM foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink").ToList().FirstOrDefault(f => f.ItemId == id);

            foodAndDrinks.Quantity = quantity;
            return View("FoodDetail", foodAndDrinks);


        }
        [HttpPost]
        public IActionResult FoodDetailCart(int id, string name,decimal price,int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();

            if (quantity > 0)
            {
                var item = cart.FirstOrDefault(t => t.ItemId == id);

                if (item != null)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Add(new FoodAndDrinkVM() { ItemId = id, ItemName = name, Price = price, Description = "", Quantity = quantity });

                }
            }
            else
            {
                var item = cart.FirstOrDefault(t => t.ItemId == id);
                if (item != null)
                {
                    cart.Remove(item);
                }

            }

            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Nếu không còn thay đổi gì thì lên đơn thôi nào.";

            HttpContext.Session.SetObjectAsJson("FoodCart", cart);
            return RedirectToAction("OrderFood");

        }
        public IActionResult GearDetail(int id, int quantity,int quantityavai)
        {

            GearVM tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears").ToList().FirstOrDefault(s=>s.GearId==id);

            tickets.Quantity = quantity;
            tickets.QuantityAvailable=quantityavai;
            return View("GearDetail", tickets);


        }
        [HttpPost]
        public IActionResult GearDetailCart(int id, string name, decimal price, int quantity,int quantityavai)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();

            if (quantity > 0)
            {
                var item = cart.FirstOrDefault(t => t.GearId == id);

                if (item != null)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Add(new GearVM { GearId = id, GearName = name, RentalPrice = price, Description = "", Quantity = quantity ,QuantityAvailable=quantityavai});

                }
            }
            else
            {
                var item = cart.FirstOrDefault(t => t.GearId == id);
                if (item != null)
                {
                    cart.Remove(item);
                }

            }

            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Nếu không còn thay đổi gì thì lên đơn thôi nào.";

            HttpContext.Session.SetObjectAsJson("GearCart", cart);
            return RedirectToAction("OrderGear");

        }
        public IActionResult FoodCart(List<int> id, List<string> name, List<decimal> price, List<int> quantity)
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
                        cart.Add(new FoodAndDrinkVM() { ItemId = id[i], ItemName = name[i], Price = price[i], Description = "", Quantity = quantity[i] });

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
            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Nếu không còn thay đổi gì thì lên đơn thôi nào.";

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
            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
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
            TempData["Notification"] = "Thêm vào giỏ hàng thành công! Nếu không còn thay đổi gì thì lên đơn thôi nào.";

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

            if (TempData["Notification"] != null)
            {
                ViewBag.Notification = TempData["Notification"];
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
            TempData["Notification"] = "Thêm vào giỏ hàng thành công.";

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
                        EmployeeId = HttpContext.Session.GetInt32("UserId"),  // Assuming this is static for now
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
                    var jwtToken = Request.Cookies["JWTToken"];
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var apiUrl = "https://localhost:7298/api/OrderManagement/CreateUniqueOrder\r\n";

                    // Serialize request object to JSON
                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");
                    Console.WriteLine(content);
                    // Make the API call
                    var response = await _httpClient.PostAsync(apiUrl, content);

                    // Process API response
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        TempData["Notification"] = "Tạo đơn thành công.";
                        // Xóa OrderCart
                        HttpContext.Session.Remove("OrderCart");

                        // Xóa TicketCart
                        HttpContext.Session.Remove("TicketCart");
                        HttpContext.Session.Remove("ComboCart");


                        // Xóa GearCart
                        HttpContext.Session.Remove("GearCart");

                        // Xóa FoodCart
                        HttpContext.Session.Remove("FoodCart");

                        // Xóa ComboFoodCart
                        HttpContext.Session.Remove("ComboFoodCart");
                        return RedirectToAction("CreateOrder");  // Return success message or redirect if needed
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        TempData["Notification"] = "Tạo đơn thất bại.";
                        return RedirectToAction("Error");  // Return success message or redirect if needed
                    }
                }
                catch (Exception ex)
                {
                    TempData["Notification"] = "Xảy ra lỗi ngoài luồng khi tạo đơn.";

                    return RedirectToAction("Error");  // Return success message or redirect if needed
                }
            }
            else
            {
                var orderRequest = new CreateComboOrderRequest
                {
                    Order = new OrderAddDTO
                    {
                        EmployeeId = HttpContext.Session.GetInt32("UserId"),  // Assuming this is static for now
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
                    var jwtToken = Request.Cookies["JWTToken"];
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var apiUrl = "https://localhost:7298/api/OrderManagement/CreateComboOrder";

                    // Serialize request object to JSON
                    var content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

                    // Make the API call
                    var response = await _httpClient.PostAsync(apiUrl, content);

                    // Process API response
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        TempData["Notification"] = "Tạo đơn thành công.";
                        HttpContext.Session.Remove("OrderCart");

                        // Xóa TicketCart
                        HttpContext.Session.Remove("TicketCart");

                        // Xóa GearCart
                        HttpContext.Session.Remove("GearCart");

                        // Xóa FoodCart
                        HttpContext.Session.Remove("FoodCart");
                        HttpContext.Session.Remove("ComboCart");

                        // Xóa ComboFoodCart
                        HttpContext.Session.Remove("ComboFoodCart");
                        return RedirectToAction("CreateOrder");  // Return success message or redirect if needed
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        TempData["Notification"] = "Tạo đơn thất bại.";
                        return RedirectToAction("Error");  // Return success message or redirect if needed
                    }
                }
                catch (Exception ex)
                {
                    TempData["Notification"] = "Xảy ra lỗi ngoài luồng khi tạo đơn.";

                    return RedirectToAction("Error");  // Return success message or redirect if needed
                }
            }

            // If no items to process, return the CreateOrder view
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
            HttpContext.Session.Remove("ComboCart");

            // Xóa FoodCart
            HttpContext.Session.Remove("FoodCart");
            TempData["Notification"] = "Đã xóa hết sản phẩm trong giỏ hàng";

            // Xóa ComboFoodCart
            HttpContext.Session.Remove("ComboFoodCart");
            return RedirectToAction("OrderOnline");

        }
        [HttpGet]
        public IActionResult OrderDetail(int id)
        {
            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                OrderDetailVM orderdata = GetDataFromApi<OrderDetailVM>($"https://localhost:7298/api/OrderManagement/GetOrderDetail/{id}");
                UpdateOrderDTO updateorder = new UpdateOrderDTO()
                {
                    OrderId = orderdata.OrderId,
                    OrderUsageDate = orderdata.OrderUsageDate,
                    TotalAmount = orderdata.TotalAmount,
                };
                if(orderdata.ActivityId==2)
                {
                    HttpContext.Session.SetObjectAsJson("order", updateorder);
                    HttpContext.Session.SetObjectAsJson("TicketUpdateCart", orderdata.OrderTicketDetails);
                    HttpContext.Session.SetObjectAsJson("ComboUpdateCart", orderdata.OrderComboDetails);
                    HttpContext.Session.SetObjectAsJson("ComboFoodUpdateCart", orderdata.OrderFoodComboDetails);
                    HttpContext.Session.SetObjectAsJson("FoodUpdateCart", orderdata.OrderFoodDetails);
                    HttpContext.Session.SetObjectAsJson("GearUpdateCart", orderdata.OrderCampingGearDetails);
                }
                return View("OrderDetail", orderdata);

            }
            catch (Exception e)
            {
                return RedirectToAction("Error");
            }




        }
        [HttpGet]
        public IActionResult UpdateTicket()
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();

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
            ViewBag.id = order.OrderId;
            ViewBag.tickets = tickets;
            return View("UpdateTicket");



        }
        [HttpPost]
        public async Task<IActionResult> UpdateTicket(List<int> TicketIds, List<decimal> Prices, List<int> Quantities)
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderTicketDetailDTO>>("TicketUpdateCart") ?? new List<OrderTicketDetailDTO>();

            decimal total = 0;
            List<OrderTicketAddlDTO> tickets = new List<OrderTicketAddlDTO>();
            for (int i = 0; i < TicketIds.Count; i++)
            {


                if (Quantities[i] > 0)
                {

                    tickets.Add(new OrderTicketAddlDTO { TicketId = TicketIds[i], OrderId = order.OrderId, Quantity = Quantities[i] });
                    total += (decimal)Quantities[i] * Prices[i];
                }



            }
            decimal totalticket = 0;
            foreach (var item in ticketscart)
            {
                totalticket += (decimal)item.Quantity.Value * item.Price;
            }

            if (tickets.Count == 0)
            {
                tickets.Add(new OrderTicketAddlDTO
                {
                    TicketId = 0,
                    OrderId = order.OrderId,
                    Quantity = 0
                });
            }
            UpdateOrderDTO orderupdate = new UpdateOrderDTO()
            {
                OrderId = order.OrderId,
                OrderUsageDate = order.OrderUsageDate,
                TotalAmount = order.TotalAmount - totalticket + total,
            };
            var apiUrl = "https://localhost:7298/api/OrderManagement/UpdateTicket";
            var apiUrl1 = "https://localhost:7298/api/OrderManagement/UpdateOrder";
            Console.WriteLine(tickets.Count);
            // Serialize request objects to JSON
            var content = new StringContent(JsonConvert.SerializeObject(tickets), Encoding.UTF8, "application/json");
            var content1 = new StringContent(JsonConvert.SerializeObject(orderupdate), Encoding.UTF8, "application/json");

            try
            {var jwtToken = Request.Cookies["JWTToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Call to UpdateTicket API
                var response = await _httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Proceed to the next API call if the first one is successful
                    var response1 = await _httpClient.PostAsync(apiUrl1, content1);

                    if (response1.IsSuccessStatusCode)
                    {
                        // Both updates were successful
                        ViewBag.ResultMessage = "Ticket and Order updated successfully!";
                        return RedirectToAction("OrderDetail", new { id = order.OrderId });
                    }
                    else
                    {
                        // Handle error for the second API call
                        var errorMessage1 = await response1.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating order: {errorMessage1}";
                        return View("ErrorView"); // Replace "ErrorView" with your actual error view
                    }
                }
                else
                {
                    // Handle error for the first API call
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating tickets: {errorMessage}";
                    return View("ErrorView"); // Replace "ErrorView" with your actual error view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }



        }
        public IActionResult UpdateGear()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderCampingGearDetailDTO>>("GearUpdateCart") ?? new List<OrderCampingGearDetailDTO>();
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            string formattedDate = order.OrderUsageDate.Value.ToString("yyyy-MM-dd");
            var jwtToken = Request.Cookies["JWTToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            List<GearVM> tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");
            List<OrderCampingGearByUsageDateDTO> ordergear = GetDataFromApi<List<OrderCampingGearByUsageDateDTO>>($"https://localhost:7298/api/OrderManagement/GetListOrderGearByUsageDate/{formattedDate}");
            foreach (var item in tickets)
            {
                var ticket = ordergear.ToList().Where(s => s.GearId == item.GearId);
                if (ticket != null)
                {
                    foreach (var item1 in ticket)
                    {
                        item.QuantityAvailable = item.QuantityAvailable - item1.Quantity.Value;
                    }
                }
            }

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.GearId == item.GearId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity.Value;
                }
            }


            ViewBag.gears = tickets;
            return View("UpdateGear");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGear(List<int> id, List<decimal> price, List<int> quantity)
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderCampingGearDetailDTO>>("GearUpdateCart") ?? new List<OrderCampingGearDetailDTO>();

            decimal total = 0;
            List<OrderCampingGearAddDTO> tickets = new List<OrderCampingGearAddDTO>();
            for (int i = 0; i < id.Count; i++)
            {


                if (quantity[i] > 0)
                {

                    tickets.Add(new OrderCampingGearAddDTO { GearId = id[i], OrderId = order.OrderId, Quantity = quantity[i] });
                    total += (decimal)quantity[i] * price[i];
                }



            }
            decimal totalticket = 0;
            foreach (var item in ticketscart)
            {
                totalticket += (decimal)item.Quantity.Value * item.Price;
            }

            if (tickets.Count == 0)
            {
                tickets.Add(new OrderCampingGearAddDTO
                {
                    GearId = 0,
                    OrderId = order.OrderId,
                    Quantity = 0
                });
            }
            UpdateOrderDTO orderupdate = new UpdateOrderDTO()
            {
                OrderId = order.OrderId,
                OrderUsageDate = order.OrderUsageDate,
                TotalAmount = order.TotalAmount - totalticket + total,
            };
            var apiUrl = "https://localhost:7298/api/OrderManagement/UpdateCampingGear";
            var apiUrl1 = "https://localhost:7298/api/OrderManagement/UpdateOrder";
            Console.WriteLine(tickets.Count);
            // Serialize request objects to JSON
            var content = new StringContent(JsonConvert.SerializeObject(tickets), Encoding.UTF8, "application/json");
            var content1 = new StringContent(JsonConvert.SerializeObject(orderupdate), Encoding.UTF8, "application/json");

            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Call to UpdateTicket API
                var response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Proceed to the next API call if the first one is successful
                    var response1 = await _httpClient.PostAsync(apiUrl1, content1);

                    if (response1.IsSuccessStatusCode)
                    {
                        // Both updates were successful
                        ViewBag.ResultMessage = "Ticket and Order updated successfully!";
                        return RedirectToAction("OrderDetail", new { id = order.OrderId });
                    }
                    else
                    {
                        // Handle error for the second API call
                        var errorMessage1 = await response1.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating order: {errorMessage1}";
                        return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                    }
                }
                else
                {
                    // Handle error for the first API call
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating tickets: {errorMessage}";
                    return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
            }

        }

        public IActionResult UpdateFood()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderFoodDetailDTO>>("FoodUpdateCart") ?? new List<OrderFoodDetailDTO>();

            List<FoodAndDrinkVM> foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");

            foreach (var item in ticketscart)
            {
                var ticket = foodAndDrinks.ToList().FirstOrDefault(s => s.ItemId == item.ItemId);
                if (ticket != null)
                {
                    ticket.Description = item.Description;
                    ticket.Quantity = item.Quantity.Value;
                }
            }


            ViewBag.gears = foodAndDrinks;
            return View("UpdateFood");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFood(List<int> id, List<decimal> price, List<int> quantity, List<string> description)
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderFoodDetailDTO>>("FoodUpdateCart") ?? new List<OrderFoodDetailDTO>();

            decimal total = 0;
            List<OrderFoodAddDTO> tickets = new List<OrderFoodAddDTO>();
            for (int i = 0; i < id.Count; i++)
            {


                if (quantity[i] > 0)
                {

                    tickets.Add(new OrderFoodAddDTO { ItemId = id[i], OrderId = order.OrderId, Quantity = quantity[i] });
                    total += (decimal)quantity[i] * price[i];
                }



            }
            decimal totalticket = 0;
            foreach (var item in ticketscart)
            {
                totalticket += (decimal)item.Quantity.Value * item.Price;
            }

            if (tickets.Count == 0)
            {
                tickets.Add(new OrderFoodAddDTO
                {
                    ItemId = 0,
                    OrderId = order.OrderId,
                    Quantity = 0
                });
            }
            UpdateOrderDTO orderupdate = new UpdateOrderDTO()
            {
                OrderId = order.OrderId,
                OrderUsageDate = order.OrderUsageDate,
                TotalAmount = (order.TotalAmount - totalticket) + total,
            };
            var apiUrl = "https://localhost:7298/api/OrderManagement/UpdateFoodAndDrink";
            var apiUrl1 = "https://localhost:7298/api/OrderManagement/UpdateOrder";
            Console.WriteLine(tickets.Count);
            // Serialize request objects to JSON
            var content = new StringContent(JsonConvert.SerializeObject(tickets), Encoding.UTF8, "application/json");
            var content1 = new StringContent(JsonConvert.SerializeObject(orderupdate), Encoding.UTF8, "application/json");

            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Call to UpdateTicket API
                var response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Proceed to the next API call if the first one is successful
                    var response1 = await _httpClient.PostAsync(apiUrl1, content1);

                    if (response1.IsSuccessStatusCode)
                    {
                        // Both updates were successful
                        ViewBag.ResultMessage = "Ticket and Order updated successfully!";
                        return RedirectToAction("OrderDetail", new { id = order.OrderId });
                    }
                    else
                    {
                        // Handle error for the second API call
                        var errorMessage1 = await response1.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating order: {errorMessage1}";
                        return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                    }
                }
                else
                {
                    // Handle error for the first API call
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating tickets: {errorMessage}";
                    return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
            }


        }


        public IActionResult UpdateComboFood()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderFoodComboDetailDTO>>("ComboFoodUpdateCart") ?? new List<OrderFoodComboDetailDTO>();

            List<ComboFoodVM> tickets = GetDataFromApi<List<ComboFoodVM>>("https://localhost:7298/api/ComboFood/GetAllOrders");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.ComboId == item.ComboId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity.Value;
                }
            }


            ViewBag.gears = tickets;
            return View("UpdateComboFood");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateComboFood(List<int> id, List<decimal> price, List<int> quantity)
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderFoodComboDetailDTO>>("ComboFoodUpdateCart") ?? new List<OrderFoodComboDetailDTO>();

            decimal total = 0;
            List<OrderFoodComboAddDTO> tickets = new List<OrderFoodComboAddDTO>();
            for (int i = 0; i < id.Count; i++)
            {


                if (quantity[i] > 0)
                {

                    tickets.Add(new OrderFoodComboAddDTO { ComboId = id[i], OrderId = order.OrderId, Quantity = quantity[i] });
                    total += (decimal)quantity[i] * price[i];
                }



            }
            decimal totalticket = 0;
            foreach (var item in ticketscart)
            {
                totalticket += (decimal)item.Quantity.Value * item.Price;
            }

            if (tickets.Count == 0)
            {
                tickets.Add(new OrderFoodComboAddDTO
                {
                    ComboId = 0,
                    OrderId = order.OrderId,
                    Quantity = 0
                });
            }
            UpdateOrderDTO orderupdate = new UpdateOrderDTO()
            {
                OrderId = order.OrderId,
                OrderUsageDate = order.OrderUsageDate,
                TotalAmount = (order.TotalAmount - totalticket) + total,
            };
            var apiUrl = "https://localhost:7298/api/OrderManagement/UpdateFoodCombo";
            var apiUrl1 = "https://localhost:7298/api/OrderManagement/UpdateOrder";

            var content = new StringContent(JsonConvert.SerializeObject(tickets), Encoding.UTF8, "application/json");
            var content1 = new StringContent(JsonConvert.SerializeObject(orderupdate), Encoding.UTF8, "application/json");

            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Call to UpdateTicket API
                var response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Proceed to the next API call if the first one is successful
                    var response1 = await _httpClient.PostAsync(apiUrl1, content1);

                    if (response1.IsSuccessStatusCode)
                    {
                        // Both updates were successful
                        ViewBag.ResultMessage = "Ticket and Order updated successfully!";
                        return RedirectToAction("OrderDetail", new { id = order.OrderId });

                    }
                    else
                    {
                        // Handle error for the second API call
                        var errorMessage1 = await response1.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating order: {errorMessage1}";
                        return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                    }
                }
                else
                {
                    // Handle error for the first API call
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating tickets: {errorMessage}";
                    return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
            }
        }
        public IActionResult UpdateCombo()
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<ComboVM>>("ComboUpdateCart") ?? new List<ComboVM>();

            List<ComboVM> tickets = GetDataFromApi<List<ComboVM>>("https://localhost:7298/api/Combo/GetAllCombos");

            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.ComboId == item.ComboId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.gears = tickets;
            return View("UpdateCombo");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCombo(List<int> id, List<decimal> price, List<int> quantity)
        {
            var order = HttpContext.Session.GetObjectFromJson<UpdateOrderDTO>("order") ?? new UpdateOrderDTO();
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<OrderComboDetailDTO>>("ComboUpdateCart") ?? new List<OrderComboDetailDTO>();

            decimal total = 0;
            List<OrderComboAddDTO> tickets = new List<OrderComboAddDTO>();
            for (int i = 0; i < id.Count; i++)
            {


                if (quantity[i] > 0)
                {

                    tickets.Add(new OrderComboAddDTO { ComboId = id[i], OrderId = order.OrderId, Quantity = quantity[i], Description = "string" });
                    total += (decimal)quantity[i] * price[i];
                }



            }
            decimal totalticket = 0;
            foreach (var item in ticketscart)
            {
                totalticket += (decimal)item.Quantity.Value * item.Price;
            }

            if (tickets.Count == 0)
            {
                tickets.Add(new OrderComboAddDTO
                {
                    ComboId = 0,
                    OrderId = order.OrderId,
                    Quantity = 0,
                    Description = "string"
                });
            }
            UpdateOrderDTO orderupdate = new UpdateOrderDTO()
            {
                OrderId = order.OrderId,
                OrderUsageDate = order.OrderUsageDate,
                TotalAmount = order.TotalAmount - totalticket + total,
            };
            var apiUrl = "https://localhost:7298/api/OrderManagement/UpdateCombo";
            var apiUrl1 = "https://localhost:7298/api/OrderManagement/UpdateOrder";
            Console.WriteLine(tickets.Count);
            // Serialize request objects to JSON
            var content = new StringContent(JsonConvert.SerializeObject(tickets), Encoding.UTF8, "application/json");
            var content1 = new StringContent(JsonConvert.SerializeObject(orderupdate), Encoding.UTF8, "application/json");

            try
            {
                var jwtToken = Request.Cookies["JWTToken"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                // Call to UpdateTicket API
                var response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Proceed to the next API call if the first one is successful
                    var response1 = await _httpClient.PostAsync(apiUrl1, content1);

                    if (response1.IsSuccessStatusCode)
                    {
                        // Both updates were successful
                        ViewBag.ResultMessage = "Ticket and Order updated successfully!";
                        return RedirectToAction("Index"); // Replace "SuccessView" with your actual view
                    }
                    else
                    {
                        // Handle error for the second API call
                        var errorMessage1 = await response1.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating order: {errorMessage1}";
                        return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                    }
                }
                else
                {
                    // Handle error for the first API call
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating tickets: {errorMessage}";
                    return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return RedirectToAction("Error"); // Replace "ErrorView" with your actual error view
            }
        }

    }
}