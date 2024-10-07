using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers.AdminController
{
    public class EmployeeManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
