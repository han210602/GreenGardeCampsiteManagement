using Microsoft.AspNetCore.Mvc;

namespace GreenGardenClient.Controllers.AdminController
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
