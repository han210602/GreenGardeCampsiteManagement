using Microsoft.AspNetCore.Mvc;

namespace GreenGardenCampsiteClient.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
