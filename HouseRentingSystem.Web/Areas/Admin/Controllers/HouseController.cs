namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels;

	public class HouseController : BaseController
	{
		private readonly IHouseService houseService;
		private readonly IAgentService agentService;
		public HouseController(IHouseService _houseService, IAgentService _agentService)
        {
			houseService = _houseService;
			agentService = _agentService;
		}
        public async Task<IActionResult> Mine()
		{
			try
			{
				string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
				MyHousesViewModel model = new MyHousesViewModel()
				{
					AddedHouses = await houseService.AllByAgentIdAsync(agentId!),
					RentedHouses = await houseService.AllByUserIdAsync(User.GetId()!)
				};
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}
	}
}
