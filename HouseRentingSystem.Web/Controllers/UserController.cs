namespace HouseRentingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Data.Models;
    using ViewModels.User;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.WebUtilities;
    using System.Text;

    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        public UserController(SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager,
            IUserStore<ApplicationUser> _userStore)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
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
    }
}
