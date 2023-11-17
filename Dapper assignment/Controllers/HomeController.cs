using Dapper_assignment.Models;
using Dapper_assignment.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Dapper_assignment.Models.Country;

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

        [HttpGet]
        public IActionResult CitiesByPopulationBetween(int minPopulation, int maxPopulation)
        {
            List<City> cityList = new CityQuery().GetCitiesByPopulationBetween(minPopulation, maxPopulation);
            return View(cityList);
        }

        [HttpGet]
        public IActionResult CitiesWithLimit(int limit)
        {
            List<City> cityList = new CityQuery().GetLimitedCities(limit);
            return View(cityList);
        }

        [HttpGet]
        public IActionResult CitiesByCountryCode(string countryCode)
        {
            List<City> cityList = new CityQuery().GetCitiesByCountryCode(countryCode);
            return View(cityList);
        }

        [HttpGet]
        public IActionResult EuropeanCountriesByLifeExpectancy()
        {
            List<Country> countryList = new CityQuery().GetEuropeanCountriesByLifeExpectancy();
            return View(countryList);
        }

        [HttpGet]
        public IActionResult CitiesInCountry(string countryCode)
        {
            List<City> cityList = new CityQuery().GetCitiesInCountry(countryCode);
            return View(cityList);
        }

        [HttpGet]
        public IActionResult CitiesByContinentWithLifeExpectancyAbove(Continent continent, float minLifeExpectancy)
        {
            List<City> cityList = new CityQuery().GetCitiesByContinentWithLifeExpectancyAbove(continent, minLifeExpectancy);
            return View(cityList);
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