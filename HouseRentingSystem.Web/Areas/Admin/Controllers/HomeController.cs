namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
