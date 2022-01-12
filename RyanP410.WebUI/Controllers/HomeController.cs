using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly RyanDbContext db;

        public HomeController(RyanDbContext db)
        {
            this.db = db;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendContactMessage([Bind("FullName", "EmailAddress", "Message")] Contact contact)
        {

            if (!ModelState.IsValid)
                return Json(new
                {
                    error = true,
                    message = "Məlumat tamlığı qorunmayıb!"
                });

            if (!contact.EmailAddress.IsEmail())
                return Json(new
                {
                    error = true,
                    message = "Xahiş olunur Email formatında daxil edin!"
                });

            if (ModelState.IsValid)
            {
                try
                {
                    await db.Contacts.AddAsync(contact);
                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = "Müraciətiniz uğurla bizə göndərildi!"
                    });
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Bilinməyən bir xəta baş verdi, bir neçə dəqiqə sonra yenidən yoxlayın!"
                    });
                }
            }

            return View();
        }
    }
}
