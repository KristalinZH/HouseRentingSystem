namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using static Common.GeneralApplicationConstants;
	using static Common.NotificationMessagesConstants;
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class BaseController : Controller
	{
		protected IActionResult GeneralError()
		{
			TempData[ErrorMessage] = "Unexpected error occured! Please try again later or contatct administrator!";
			return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
		}
	}
}
