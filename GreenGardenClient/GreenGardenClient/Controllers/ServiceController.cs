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

        public IActionResult OrderTicket()
        {
            return View();
        }
        public IActionResult UpdateProfile()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}
