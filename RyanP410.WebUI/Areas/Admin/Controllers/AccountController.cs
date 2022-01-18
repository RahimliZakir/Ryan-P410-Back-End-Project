using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly SignInManager<RyanUser> signInManager;

        public AccountController(SignInManager<RyanUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        async public Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home", new
            {
                area = ""
            });
        }
    }
}
