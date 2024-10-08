using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers.AdminController
{
    public class CustomerManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
