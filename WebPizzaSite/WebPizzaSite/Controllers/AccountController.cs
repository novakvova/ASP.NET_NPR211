using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebPizzaSite.Data.Entities.Identity;
using WebPizzaSite.Models.Account;

namespace WebPizzaSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountController(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null) 
            {
                var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

                if (res.Succeeded) 
                { 
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }
            }

            ModelState.AddModelError("", "Дані вказано не вірно!");

            return View(model);
        }
    }
}
