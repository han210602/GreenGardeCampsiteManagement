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

        public IActionResult UpdateOrder()
        {
            return View("UpdateOrder");
        }

    }
}
