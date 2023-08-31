
namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
	using Infrastructure.Extensions;
	using Services.Data.Interfaces;
    using ViewModels.Agent;
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class AgentController : BaseController
    {
		private readonly IAgentService agentService;
		public AgentController(IAgentService _agentService)
		{
			agentService = _agentService;
		}
		[HttpGet]
        public async Task<IActionResult> Become()
        {
            try
            {
                string? userId = User.GetId();
                bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);
                if (isAgent)
                {
                    TempData[ErrorMessage] = "You are already and agent!";
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            try
            {
                string? userId = User.GetId();
                bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);
                if (isAgent)
                {
                    TempData[ErrorMessage] = "You are already and agent!";
                    return RedirectToAction("Index", "Home");
                }
                bool isPhoneNumberTaken = await agentService.AgentExistsByPhoneNumberAsync(model.PhoneNumber);
                if (isPhoneNumberTaken)
                {
                    ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exists!");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                bool userHasActiveRents = await agentService.HasRentsByUserIdAsync(userId!);
                if (userHasActiveRents)
                {
                    TempData[ErrorMessage] = "You must not have any active rents in order to become an agent!";
                    return RedirectToAction("Mine", "House");
                }
                await agentService.Create(userId!, model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
            return RedirectToAction("All","House");
        }
    }
}
