using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.ClientsModule;
using RyanP410.WebUI.AppCode.Modules.ContactsModule;
using RyanP410.WebUI.AppCode.Modules.FunFactsModule;
using RyanP410.WebUI.AppCode.Modules.PersonsModule;
using RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule;
using RyanP410.WebUI.AppCode.Modules.ServicesModule;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        async public Task<IActionResult> Index()
        {
            AboutViewModel vm = new AboutViewModel();

            var personSingleQuery = new PersonSingleQuery();
            var person = await mediator.Send(personSingleQuery);

            var servicesQuery = new ServicesQuery();
            var services = await mediator.Send(servicesQuery);

            var pricingCollectionsQuery = new PricingsPricingDetailsUserSideQuery();
            var pricingCollections = await mediator.Send(pricingCollectionsQuery);

            var funFactsQuery = new FunFactsQuery();
            var funFacts = await mediator.Send(funFactsQuery);

            var clientsQuery = new ClientsQuery();
            var clients = await mediator.Send(clientsQuery);

            vm.Person = person;
            vm.Services = services;
            vm.Pricings = pricingCollections;
            vm.FunFacts = funFacts;
            vm.Clients = clients;

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendContactMessage([Bind("FullName", "EmailAddress", "Message")] ContactSendMessageCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
