using Dapper_assignment.Models;
using Dapper_assignment.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dapper_assignment.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CityQuery()
        {
            List<City> cityList = new CityQuery().GetCities(0, 10000);

            return View(cityList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}