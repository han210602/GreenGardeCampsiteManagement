using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers.AdminController
{
    public class OrderManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            return View("CreateOrder");
        }

        public IActionResult OrderCampingGears()
        {
            return View("OrderCampingGears");
        }

        public IActionResult OrderFoodAndDrinks()
        {
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

    }
}
