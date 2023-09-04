namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
	using HouseRentingSystem.Web.ViewModels.User;
	using Microsoft.AspNetCore.Mvc;
	using Services.Data.Interfaces;

	public class UserController : BaseController
	{
		private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
			userService = _userService;
		}
		[Route("User/All")]
		public async Task<IActionResult> All()
		{
			try
			{
				IEnumerable<UserViewModel> model = await userService.AllAsync();
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}
	}
}
