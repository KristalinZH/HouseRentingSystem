namespace HouseRentingSystem.Services.Data
{
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using HouseRentingSystem.Data;
    using HouseRentingSystem.Data.Models;
    using Web.ViewModels.Agent;
    using Interfaces;

    public class AgentService : IAgentService
	{
		private readonly HouseRentingDbContext dbContext;
		public AgentService(HouseRentingDbContext _dbContext)
		{
			dbContext = _dbContext;
		}


        public async Task<bool> AgentExistsByUserIdAsync(string userId)
		=> await dbContext
			.Agents
			.AnyAsync(a => a.UserId.ToString() == userId);
        public async Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber)
        => await dbContext
            .Agents
            .AnyAsync(a => a.PhoneNumber == phoneNumber);

        public async Task<bool> HasRentsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
                return false;
            return user.RentedHouses.Any();
        }
        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            Agent agent = new Agent()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };
            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();
        }

		public async Task<string?> GetAgentIdByUserIdAsync(string userId)
		{
            Agent? agent = await dbContext.Agents.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agent == null)
                return null;
            return agent.Id.ToString();
		}
	}
}
