
namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Data.Interfaces;
	using Services.Data.Models.House;
    using ViewModels.House;
    using Infrastructure.Extensions;
    using static Common.NotificationMessagesConstants;

	[Authorize]
    public class HouseController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
		private readonly IHouseService houseService;
        private readonly IUserService userService;
		public HouseController(ICategoryService _categoryService, 
            IAgentService _agentService, 
            IHouseService _houseService,
			IUserService _userService)
        {
            categoryService = _categoryService;
            agentService = _agentService;
            houseService = _houseService;
            userService = _userService;
		}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel queryModel)
        {
            try
            {
                AllHousesFilteredAndPagedServiceModel serviceModel = await houseService.AllAsync(queryModel);
                queryModel.Houses = serviceModel.Houses;
                queryModel.TotalHouses = serviceModel.TotalHousesCount;
                queryModel.Categories = await categoryService.AllCategoryNamesAsync();
                return View(queryModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {           
			try
			{
                bool isAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to add new houses!";
                    return RedirectToAction("Become", "Agent");
                }
                HouseFormModel formModel = new HouseFormModel()
				{
					Categories = await categoryService.AllCategoriesAsync()
				};
				return View(formModel);
			}
			catch (Exception)
			{
                return GeneralError();
			}
		}

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {			
            try
            {
                bool isAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to add new houses!";
                    return RedirectToAction("Become", "Agent");
                }
                bool categoryExists = await categoryService.ExistsByIdAsync(model.CategoryId);
                if (!categoryExists)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist");
                }
                if (!ModelState.IsValid)
                {
                    model.Categories = await categoryService.AllCategoriesAsync();
                    return View(model);
                }
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                string houseId = await houseService.CreateAndReturnIdAsync(model, agentId!);
                TempData[SuccessMessage] = "House was added successfuly";
                return RedirectToAction("Details", "House", new { id = houseId });
			}
            catch(Exception)
            {
                return GeneralError();
            }    
		}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData[ErrorMessage] = "House with the provided id does not exist!";
                    return RedirectToAction("All", "House");
                }
                HouseDetailsViewModel viewModel = await houseService.GetDetailsByIdAsync(id);
                viewModel.Agent.FullName = await userService.GetFullNameByEmailAsync(viewModel.Agent.Email);
                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {   
            try
            {
                List<HouseAllViewModel> myHouses = new List<HouseAllViewModel>();
                string userId = User.GetId()!;
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(userId);
                if (isUserAgent)
				{
					string? agentId =
						await agentService.GetAgentIdByUserIdAsync(userId);
					myHouses.AddRange(await houseService.AllByAgentIdAsync(agentId!));
				}
				else
				{
					myHouses.AddRange(await houseService.AllByUserIdAsync(userId));
				}
				return View(myHouses);
			}
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData[ErrorMessage] = "House with the provided id does not exist!";
                    return RedirectToAction("All", "House");
                }
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isUserAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to edit house info!";
                    return RedirectToAction("Become", "Agent");
                }
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
                if (!isAgentOwner)
                {
                    TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                    return RedirectToAction("Mine", "House");
                }
                HouseFormModel formModel = await houseService.GetHouseForEditByIdAsync(id);
                formModel.Categories = await categoryService.AllCategoriesAsync();
                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
		}

        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Categories = await categoryService.AllCategoriesAsync();
                    return View(model);
                }
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData[ErrorMessage] = "House with the provided id does not exist!";
                    return RedirectToAction("All", "House");
                }
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isUserAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to edit house info!";
                    return RedirectToAction("Become", "Agent");
                }
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
                if (!isAgentOwner)
                {
                    TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                    return RedirectToAction("Mine", "House");
                }
                await houseService.EditHouseByIdAndFormModel(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to update the house. Please try again later or contact administrator!");
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }
            TempData[SuccessMessage] = "House was edited successfuly";
            return RedirectToAction("Details", "House", new { id });
        }

        [HttpGet]
		public async Task<IActionResult> Delete(string id)
        {	
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData[ErrorMessage] = "House with the provided id does not exist!";
                    return RedirectToAction("All", "House");
                }
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isUserAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to edit house info!";
                    return RedirectToAction("Become", "Agent");
                }
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
                if (!isAgentOwner)
                {
                    TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                    return RedirectToAction("Mine", "House");
                }
                HousePreDeleteDetailsViewModel viewModel = await houseService.GetHouseForDeleteByIdAsync(id);
                return View(viewModel);
            }
            catch
            {
				return GeneralError();
			}
		}

        [HttpPost]
		public async Task<IActionResult> Delete(string id,HousePreDeleteDetailsViewModel model)
        {			
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData[ErrorMessage] = "House with the provided id does not exist!";
                    return RedirectToAction("All", "House");
                }
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (!isUserAgent)
                {
                    TempData[ErrorMessage] = "You must become an agent in order to edit house info!";
                    return RedirectToAction("Become", "Agent");
                }
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
                if (!isAgentOwner)
                {
                    TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                    return RedirectToAction("Mine", "House");
                }
                await houseService.DeleteHouseByIdAsync(id);
                TempData[WarningMessage] = "The house was successfully deleted!";
                return RedirectToAction("Mine", "House");
            }
            catch (Exception)
            {
                return GeneralError();
			}
		}
        [HttpPost]
        public async Task<IActionResult>Rent(string id)
        {          
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData["ErrorMessage"] = "House with provided id does not exists! Please try again!";
                    return RedirectToAction("All", "House");
                }
                bool isHouseRented = await houseService.IsRentedAsync(id);
                if (isHouseRented)
                {
                    TempData["ErrorMessage"] = "Selected house is already rented by another user! Please select another house!";
                    return RedirectToAction("All", "House");
                }
                bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
                if (isUserAgent)
                {
                    TempData["ErrorMessage"] = "Agents can't rent houses. Please register as a user!";
                    return RedirectToAction("Index", "Home");
                }
                await houseService.RentHouseAsync(id, User.GetId()!);
            }
            catch (Exception)
            {
                return GeneralError();
            }
            return RedirectToAction("Mine","House");
        }
        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {            
            try
            {
                bool houseExists = await houseService.ExistsByIdAsync(id);
                if (!houseExists)
                {
                    TempData["ErrorMessage"] = "House with provided id does not exists! Please try again!";
                    return RedirectToAction("All", "House");
                }
                bool isHouseRented = await houseService.IsRentedAsync(id);
                if (!isHouseRented)
                {
                    TempData["ErrorMessage"] = "Selected house is not rented! Please select one of you houses if you wish to leave them!";
                    return RedirectToAction("Mine", "House");
                }
                bool isCurrentUserRenterOfTheHouse = await houseService
                    .IsRentedByUserWithIdAsync(id, User.GetId()!);
                if (!isCurrentUserRenterOfTheHouse)
                {
                    TempData["ErrorMessage"] = "You must be the renter of the house in order to leave it! Please try again with one of your rented house if you wish to leave it.";
                    return RedirectToAction("Mine", "House");
                }
                await houseService.LeaveHouseAsync(id);
            }
            catch (Exception)
            {
                return GeneralError();
            }
            return RedirectToAction("Mine", "House");
        }
    }
}
