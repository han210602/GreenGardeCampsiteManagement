using GreenGardenClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GreenGardenClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Remove the JWT token cookie
            Response.Cookies.Delete("JWTToken");

             return RedirectToAction("Index");
        }



    }
}