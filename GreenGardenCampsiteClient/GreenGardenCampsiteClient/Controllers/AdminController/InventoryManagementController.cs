using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers.AdminController
{
    public class InventoryManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
