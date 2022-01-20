using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities.Membership;
using RyanP410.WebUI.Models.FormModels;
using System.Text.RegularExpressions;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RyanP410.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly RyanDbContext db;
        private readonly UserManager<RyanUser> userManager;
        private readonly SignInManager<RyanUser> signInManager;

        public AccountController(RyanDbContext db, UserManager<RyanUser> userManager, SignInManager<RyanUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        async public Task<IActionResult> SignIn(SignInFormModel formModel)
        {
            Regex pattern = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            RyanUser userResult = null;

            if (pattern.IsMatch(formModel.Username))
            {
                userResult = await userManager.FindByEmailAsync(formModel.Username);
            }
            else
            {
                userResult = await userManager.FindByNameAsync(formModel.Username);
            }

            if (userResult != null)
            {
                SignInResult signInResult = await signInManager.PasswordSignInAsync(userResult, formModel.Password, false, false);

                if (signInResult.Succeeded)
                {
                    string returlUrl = HttpContext.Request.Query["returnUrl"];

                    if (!string.IsNullOrWhiteSpace(returlUrl))
                    {
                        return Redirect(returlUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), "PersonalSide", new
                        {
                            area = "Admin"
                        });
                    }
                }
                else
                {
                    ModelState.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                    TempData["SingInError"] = "İstifadəçi adı və ya şifrə səhvdir.";
                }
            }
            else
            {
                ModelState.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                TempData["SingInError"] = "İstifadəçi adı və ya şifrə səhvdir.";
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> Register(RegisterFormModel formModel)
        {
            var appUser = new RyanUser
            {
                UserName = formModel.Username,
                Email = formModel.Email
            };

            var appResponse = await userManager.CreateAsync(appUser, formModel.Password);

            if (appResponse.Succeeded)
            {
                return Redirect(@"\signin.html");
            }
            else
            {
                foreach (IdentityError error in appResponse.Errors)
                {
                    ModelState.AddModelError("RegisterError", error.Description);
                }
            }

            return View();
        }

        public IActionResult AccessDenied()
        {

            return View();
        }
    }
}
