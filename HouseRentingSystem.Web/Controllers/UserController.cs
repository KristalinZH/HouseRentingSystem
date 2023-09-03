namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Data.Models;
    using ViewModels.User;
    using static Common.NotificationMessagesConstants;
    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                ApplicationUser user = new ApplicationUser()
                {
                   FirstName=model.FirstName,
                   LastName=model.LastName
                };
                await userManager.SetUserNameAsync(user, model.Email);
                await userManager.SetEmailAsync(user, model.Email);
                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl=returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model, string? returnUrl = null)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    TempData[ErrorMessage] = "There was an error while loging you in! Please try again later or contact administrator!";
                    return View(model);
                }
                return Redirect(model.ReturnUrl ?? "/Home/Index");
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
    }
}
