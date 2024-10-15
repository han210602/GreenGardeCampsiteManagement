using Microsoft.AspNetCore.Mvc;

namespace GreenGardenClient.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderGear()
        {
            return View();
        }
        public IActionResult OrderFoodAndDrink()
        {
            return View();
        }
        public IActionResult FoodDetail()
        {
            return View();
        }
    }
}
