using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers.AdminController
{
    public class EventManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
