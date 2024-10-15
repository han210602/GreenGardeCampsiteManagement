using Microsoft.AspNetCore.Mvc;

namespace GreenGardenClient.Controllers
{
    public class CommonController : Controller
    {
        private readonly ILogger<CommonController> _logger;

        public CommonController(ILogger<CommonController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
