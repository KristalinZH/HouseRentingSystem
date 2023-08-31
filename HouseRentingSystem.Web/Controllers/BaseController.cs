namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static Common.NotificationMessagesConstants;
    public class BaseController : Controller
    {
        protected IActionResult GeneralError()
        {
            TempData[ErrorMessage] = "Unexpected error occured! Please try again later or contatct administrator!";
            return RedirectToAction("Index", "Home");
        }
    }
}
