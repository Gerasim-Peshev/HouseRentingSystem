using HouseRentingSystem.Contacts.Agent;
using HouseRentingSystem.Contacts.House;
using HouseRentingSystem.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HouseRentingSystem.Services.House.Models;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(IHouseService houses, IAgentService agents)
        {
            this.houseService = houses;
            this.agentService = agents;
        }

        public string UserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = houseService.All(query.Category, 
                                               query.SearchTerm, 
                                               query.Sorting, 
                                               query.CurrentPage,
                                               AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = await houseService.AllCategiriesNames();
            query.Categories = (IEnumerable<string>) houseCategories;

            return View(query);
        }

        public async Task<IActionResult> MyHouses()
        {
            IEnumerable<HouseServiceModel> myHouses;

            var userId = UserId();

            if (await agentService.ExistsById(userId))
            {
                var currentAgent = await agentService.GetAgentById(userId);

                myHouses = await houseService.AllHousesByAgentId(currentAgent.Id);
            }
            else
            {
                myHouses = await houseService.AllHousesByUserId(userId);
            }

            return View(myHouses);
        }

        [HttpGet]
        public async Task<IActionResult> AddHouse()
        {
            if (await agentService.ExistsById(UserId()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            return View(new AddHouseViewModel()
            {
                Categories = await houseService.AllCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(AddHouseViewModel houseToAdd)
        {
            if (await agentService.ExistsById(UserId()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await houseService.CategoryExists(houseToAdd.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(houseToAdd.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                houseToAdd.Categories = await houseService.AllCategories();

                return View(houseToAdd);
            }

            var agent = await agentService.GetAgentById(UserId());

            var newHouseId = await houseService.Create(houseToAdd.Title, houseToAdd.Address, houseToAdd.Description,
                                                     houseToAdd.ImageUrl, houseToAdd.PricePerMonth,
                                                     houseToAdd.CategoryId, agent.Id);

            return RedirectToAction(nameof(Details), new {id = newHouseId});
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            var houseModel = await houseService.HouseDetailsById(id);

            return View(houseModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, this.UserId()) == false)
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsById(id);
            var houseCategoryId = await houseService.GetHouseCategoryId(house.Id);

            var houseModel = new AddHouseViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = houseCategoryId,
                Categories = await houseService.AllCategories()
            };

            return View(houseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddHouseViewModel houseToEdit)
        {
            if (await houseService.Exists(id) == false)
            {
                return this.View();
            }

            if (await houseService.HasAgentWithId(id, UserId()) == false)
            {
                return Unauthorized();
            }

            if (await houseService.CategoryExists(houseToEdit.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(houseToEdit.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                houseToEdit.Categories = await houseService.AllCategories();

                return View(houseToEdit);
            }

            await houseService.Edit(id, houseToEdit.Title, houseToEdit.Address, houseToEdit.Description, houseToEdit.ImageUrl, houseToEdit.PricePerMonth, houseToEdit.CategoryId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, UserId()) == false)
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsById(id);

            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel houseToDelete)
        {
            if (await houseService.Exists(houseToDelete.Id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(houseToDelete.Id, UserId()) == false)
            {
                return Unauthorized();
            }

            await houseService.Delete(houseToDelete.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await agentService.ExistsById(UserId()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRented(id))
            {
                return BadRequest();
            }

            await houseService.Rent(id, UserId());

            return RedirectToAction(nameof(MyHouses));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await houseService.Exists(id) == false || await houseService.IsRented(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.IsRentedByUserWithId(id, UserId()) == false)
            {
                return Unauthorized();
            }

            await houseService.Leave(id);

            return RedirectToAction(nameof(MyHouses));
        }
    }
}
