using Microsoft.AspNetCore.Mvc;

namespace GreenGardenClient.Controllers.AdminController
{
    public class EmployeeManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
