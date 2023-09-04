
namespace HouseRentingSystem.Web.Controllers
{
	using HouseRentingSystem.Web.Infrastructure.Extensions;
	using Microsoft.AspNetCore.Mvc;
    using Services.Data.Interfaces;
	using ViewModels.Home;
	using static Common.GeneralApplicationConstants;
	public class HomeController : BaseController
	{
		private readonly IHouseService houseService;
		public HomeController(IHouseService _houseService)
		{
			houseService = _houseService;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				if((User?.Identity?.IsAuthenticated ?? false) && User.IsAdmin())
				{
					return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
				}
				IEnumerable<IndexViewModel> viewModel = await houseService.LastThreeHousesAsync();
				return View(viewModel);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statuscode)
		{
			if (statuscode==400 || statuscode == 404)
			{
				return View("Error404");
			}
			if (statuscode == 401)
			{
				return View("Error401");
			}
			return View();
		}
	}
}