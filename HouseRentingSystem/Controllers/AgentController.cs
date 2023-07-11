using System.Security.Claims;
using HouseRentingSystem.Contacts.Agent;
using HouseRentingSystem.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agents)
        {
            this.agentService = agents;
        }

        public string UserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var userId = UserId();

            if (await agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentViewModel agentToBecome)
        {
            var userId = UserId();

            if (await agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            if (await agentService.UserWithPhoneNumberExists(agentToBecome.PhoneNumber))
            {
                ModelState.AddModelError(nameof(agentToBecome.PhoneNumber),
                    "Phone number already exist. Enter another one.");
            }

            if (await agentService.UserHasRents(userId))
            {
                ModelState.AddModelError("Error",
                    "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(agentToBecome);
            }

            await agentService.Create(userId, agentToBecome.PhoneNumber);

            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
