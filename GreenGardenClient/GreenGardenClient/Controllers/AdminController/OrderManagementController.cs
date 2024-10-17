using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
            List<OrderVM> orderdata = GetDataFromApi<List<OrderVM>>("https://localhost:7298/api/Order/GetAllOrders");
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

                string apiUrl = $"https://localhost:7298/api/Order/UpdateActivityOrder/{idorder}/{idactivity}";


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
                // Construct the API URL with the parameters
                string apiUrl = $"https://localhost:7298/api/Order/EnterDeposit/{idorder}/{money}";

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
                // Construct the API URL with the parameters
                string apiUrl = $"https://localhost:7298/api/Order/CancelDeposit/{idorder}";

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
            ViewBag.name = orders.CustomerName;
            ViewBag.usagedate = orders.OrderUsageDate;
            return View("CreateOrder");


        }
        [HttpPost]
        public IActionResult CreateOrder(string name, DateTime usagedate, string phone)
        {
            var cart = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            cart = new OrderVM()
            {
                CustomerName = name,
                OrderUsageDate = usagedate,

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
            TempData["Notification"] = "Lưu thành công!";
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
        public IActionResult Cart()
        {
            var tickets = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();
            var gears = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
            var foods = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("FoodCart") ?? new List<FoodAndDrinkVM>();

            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("OrderCart") ?? new OrderVM();
            ViewBag.tickets = tickets;
            ViewBag.gears = gears;
            ViewBag.foods = foods;
            ViewBag.order = orders;
            return View("Cart");
        }

    }
}
