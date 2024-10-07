using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers
{
    public class adController : Controller
    {
        public IActionResult CustomerManagement()
        {
            return View();
        }

        public IActionResult EmployeeManagement()
        {
            return View();
        }

        public IActionResult InventoryManagement()
        {
            return View();
        }

        public IActionResult OrderManagement()
        {
            return View();
        }

        public IActionResult EventManagement()
        {
            return View();
        }
    }
}
