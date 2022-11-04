using ECommerceShoppingStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceShoppingStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult HomeCommon()
        {
            return View();
        }

        public IActionResult HomeAdmin()
        {
            return View();
        }

        public IActionResult HomeCustomer()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}