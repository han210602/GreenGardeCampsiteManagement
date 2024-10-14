using GreenGardenCampsiteClient.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult OrderTickets(int idcategory)
        {

            List<TicketCategoryVM> categories = GetDataFromApi<List<TicketCategoryVM>>("https://localhost:7298/api/Ticket/GetAllTicketCategories");
            List<TicketVM> tickets = GetDataFromApi<List<TicketVM>>("https://localhost:7298/api/Ticket/GetAllTickets");
            Console.WriteLine("kkkkkkkkkk" + idcategory);
            if (idcategory != 0)
            {
                tickets = GetDataFromApi<List<TicketVM>>($"https://localhost:7298/api/Ticket/GetTicketsByCategory/{idcategory}");
                ViewBag.idcategory = idcategory;
            }



            ViewBag.categories = categories;
            ViewBag.tickets = tickets;





            return View("OrderTickets");
        }

        public IActionResult OrderCampingGears(int idcategory)
        {
            List<GearCategoryVM> categories = GetDataFromApi<List<GearCategoryVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGearCategories");
            List<GearVM> tickets = GetDataFromApi<List<GearVM>>("https://localhost:7298/api/CampingGear/GetAllCampingGears");
            Console.WriteLine("kkkkkkkkkk" + idcategory);
            if (idcategory != 0)
            {
                tickets = GetDataFromApi<List<GearVM>>($"https://localhost:7298/api/CampingGear/GetCampingGearsByCategoryId/{idcategory}");
                ViewBag.idcategory = idcategory;
            }



            ViewBag.categories = categories;
            ViewBag.gears = tickets;
            return View("OrderCampingGears");
        }

        public IActionResult OrderFoodAndDrinks(int idcategory)
        {
            List<FoodAndDrinkCategoryVM> categories = GetDataFromApi<List<FoodAndDrinkCategoryVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrinkCategories\r\n");
            List<FoodAndDrinkVM> foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>("https://localhost:7298/api/FoodAndDrink/GetAllFoodAndDrink");
            Console.WriteLine("kkkkkkkkkk" + idcategory);
            if (idcategory != 0)
            {
                foodAndDrinks = GetDataFromApi<List<FoodAndDrinkVM>>($"https://localhost:7298/api/FoodAndDrink/GetFoodAndDrinkByCategory/{idcategory}");
                ViewBag.idcategory = idcategory;
            }



            ViewBag.categories = categories;
            ViewBag.gears = foodAndDrinks;
            return View("OrderFoodAndDrinks");
        }

        

        public IActionResult OrderDetails()
        {
            return View("OrderDetails");
        }


    }
}
