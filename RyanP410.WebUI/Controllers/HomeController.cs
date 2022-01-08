using Microsoft.AspNetCore.Mvc;

namespace RyanP410.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
