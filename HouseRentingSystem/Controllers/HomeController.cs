using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HouseRentingSystem.Models.Home;
using HouseRentingSystem.Contacts.House;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService houses)
        {
            this.houseService = houses;
        }
        public async Task<IActionResult> Index()
        {
            var houses = await houseService.LastThreeHouses();
            return View(houses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}