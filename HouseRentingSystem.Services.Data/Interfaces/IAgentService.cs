namespace HouseRentingSystem.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using Web.ViewModels.Agent;

	public interface IAgentService
	{
		Task<bool> AgentExistsByUserIdAsync(string userId);
		Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> HasRentsByUserIdAsync(string userId);
        Task<string?> GetAgentIdByUserIdAsync(string userId);
        Task Create(string userId, BecomeAgentFormModel model);
    }
}
