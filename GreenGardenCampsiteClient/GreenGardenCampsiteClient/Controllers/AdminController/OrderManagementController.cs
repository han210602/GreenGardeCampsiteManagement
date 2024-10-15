using GreenGardenCampsiteClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenGardenCampsiteClient.Controllers.AdminController
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
        public IActionResult Index()
        {
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
        public IActionResult OrderTickets(int idcategory, string msg)
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();


            List<TicketCategoryVM> categories = GetDataFromApi<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");
            List<TicketVM> tickets = GetDataFromApi<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");
            if (idcategory != 0)
            {
                tickets = GetDataFromApi<List<TicketVM>>($"https://localhost:7298/api/Ticket/GetTicketsByCategory/{idcategory}");
                ViewBag.idcategory = idcategory;



            }
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
            ViewBag.categories = categories;
            ViewBag.tickets = tickets;
            return View("OrderTickets");
        }
        public IActionResult AddToCart(List<int> TicketIds, List<string> Name, List<decimal> Prices, List<int> Quantities)
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
            return RedirectToAction("OrderTickets");
        }
        public IActionResult OrderCampingGears(int idcategory)
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();

            List<GearCategoryVM> categories = GetDataFromApi<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");
            List<GearVM> tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");
            if (idcategory != 0)
            {
                tickets = GetDataFromApi<List<GearVM>>($"https://localhost:7298/api/CampingGear/GetCampingGearsByCategoryId/{idcategory}");
                ViewBag.idcategory = idcategory;
            }
            foreach (var item in ticketscart)
            {
                var ticket = tickets.ToList().FirstOrDefault(s => s.GearId == item.GearId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.categories = categories;
            ViewBag.gears = tickets;
            return View("OrderCampingGears");
        }

        public IActionResult OrderFoodAndDrinks(int idcategory)
        {
            var ticketscart = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("ItemCart") ?? new List<FoodAndDrinkVM>();

            List<FoodAndDrinkCategoryVM> categories = GetDataFromApi<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories\r\n");
            List<FoodAndDrinkVM> foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");
            Console.WriteLine("kkkkkkkkkk" + idcategory);
            if (idcategory != 0)
            {
                foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>($"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinkByCategory/{idcategory}");
                ViewBag.idcategory = idcategory;
            }
            foreach (var item in ticketscart)
            {
                var ticket = foodAndDrinks.ToList().FirstOrDefault(s => s.ItemId == item.ItemId);
                if (ticket != null)
                {
                    ticket.Quantity = item.Quantity;
                }
            }


            ViewBag.categories = categories;
            ViewBag.gears = foodAndDrinks;
            return View("OrderFoodAndDrinks");
        }

        public IActionResult UpdateOrder()
        {
            return View("UpdateOrder");
        }

        public IActionResult OrderDetails()
        {
            return View("OrderDetails");
        }


        public IActionResult AddCartCampingGear(List<int> id, List<string> name, List<decimal> price, List<int> quantity)
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
            return RedirectToAction("OrderCampingGears");
        }
        public IActionResult AddCartItem(List<int> id, List<string> name, List<decimal> price, List<int> quantity, List<string> description)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("ItemCart") ?? new List<FoodAndDrinkVM>();


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
            HttpContext.Session.SetObjectAsJson("ItemCart", cart);
            return RedirectToAction("OrderFoodAndDrinks");
        }
        public IActionResult Cart()
        {
            var tickets = HttpContext.Session.GetObjectFromJson<List<TicketVM>>("TicketCart") ?? new List<TicketVM>();
            var gears = HttpContext.Session.GetObjectFromJson<List<GearVM>>("GearCart") ?? new List<GearVM>();
            var items = HttpContext.Session.GetObjectFromJson<List<FoodAndDrinkVM>>("ItemCart") ?? new List<FoodAndDrinkVM>();

            var orders = HttpContext.Session.GetObjectFromJson<OrderVM>("order") ?? new OrderVM();
            ViewBag.tickets = tickets;
            ViewBag.gears = gears;
            ViewBag.items = items;
            ViewBag.orders = orders;
            return View("Cart");
        }
    }


}
public static class SessionExtensions
{
    // Method to store an object as a JSON string in the session
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // Serialize the object to JSON and store it in the session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    // Method to retrieve an object from JSON string in the session
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        // Get the JSON string from the session
        var value = session.GetString(key);
        // Deserialize the JSON string back to the object
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}

