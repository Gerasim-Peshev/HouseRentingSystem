using HouseRentingSystem.Contacts.Agent;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Agent
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext context;

        public AgentService(HouseRentingDbContext housesDbContext)
        {
            this.context = housesDbContext;
        }
        public async Task<bool> ExistsById(string userId)
        {
            return await context.Agents.AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await context.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return await context.Houses.AnyAsync(h => h.RenterId == userId);
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Data.Models.Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync();
        }

        public async Task<Data.Models.Agent> GetAgentById(string userId)
        {
            return await context.Agents.FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}
