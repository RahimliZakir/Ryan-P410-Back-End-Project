using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.ContactsModule;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        readonly RyanDbContext db;
        readonly IMediator mediator;
        readonly IConfiguration conf;

        public ContactsController(RyanDbContext db, IMediator mediator, IConfiguration conf)
        {
            this.db = db;
            this.mediator = mediator;
            this.conf = conf;
        }

        async public Task<IActionResult> Index(bool visibility)
        {
            ContactsQuery query = new();

            TempData["Vis"] = "";

            if (visibility)
            {
                IEnumerable<Contact> answeredContactMessages = await mediator.Send(query);

                answeredContactMessages = await db.Contacts
                                                .Where(cm => !string.IsNullOrWhiteSpace(cm.AnswerMessage))
                                                .ToListAsync();

                TempData["Vis"] = "checked";

                return View(answeredContactMessages);
            }
            else
            {
                IEnumerable<Contact> notAnsweredContactMessages = await mediator.Send(query);

                notAnsweredContactMessages = await db.Contacts
                                                   .Where(cm => string.IsNullOrWhiteSpace(cm.AnswerMessage))
                                                   .ToListAsync();

                TempData["Vis"] = "";

                return View(notAnsweredContactMessages);
            }
        }

        async public Task<IActionResult> Answer(ContactSingleQuery query)
        {
            Contact notAnsweredMessage = await mediator.Send(query);

            AnswerContactFormModel formModel = new()
            {
                Id = notAnsweredMessage.Id,
                FullName = notAnsweredMessage.FullName,
                EmailAddress = notAnsweredMessage.EmailAddress,
                Message = notAnsweredMessage.Message
            };

            return View(formModel);
        }

        [HttpPost]
        async public Task<IActionResult> Answer(ContactAnswerCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
