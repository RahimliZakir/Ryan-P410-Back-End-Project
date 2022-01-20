using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.PricingDetailsModule;
using RyanP410.WebUI.AppCode.Modules.PricingsModule;
using RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PricingsPricingDetailsCollectionsController : Controller
    {
        readonly IMediator mediator;

        public PricingsPricingDetailsCollectionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            var query = new PricingsPricingDetailsQuery();
            var data = await mediator.Send(query);

            return View(data);
        }

        async public Task<IActionResult> Details(PricingsPricingDetailsSingleQuery query)
        {
            var collection = await mediator.Send(query);

            return View(collection);
        }

        public async Task<IActionResult> Create()
        {
            PricingsQuery pricingsQuery = new();
            IEnumerable<Pricing> pricings = await mediator.Send(pricingsQuery);
            ViewBag.Pricings = new SelectList(pricings, "Id", "Title");

            PricingDetailsQuery pricingDetailsQuery = new();
            IEnumerable<PricingDetail> details = await mediator.Send(pricingDetailsQuery);
            ViewBag.PricingDetails = new SelectList(details, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PricingsPricingDetailsCreateCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            if (!response.Error)
            {
                return Json(response);
            }

            return View(request);
        }

        async public Task<IActionResult> Edit(PricingsPricingDetailsSingleQuery query)
        {
            var collection = await mediator.Send(query);

            var viewModel = new PricingsPricingDetailsViewModel
            {
                Id = collection.Id,
                PricingDetailId = collection.PricingDetailId,
                PricingId = collection.PricingId,
                Exists = collection.Exists,
                New = collection.New
            };

            return View(viewModel);
        }

        [HttpPost]
        async public Task<IActionResult> Edit(PricingsPricingDetailsEditCommand request)
        {
            //var collection = await mediator.Send(query);

            //var viewModel = new PricingsPricingDetailsViewModel
            //{
            //    Id = collection.Id,
            //    PricingDetailId = collection.PricingDetailId,
            //    PricingId = collection.PricingId,
            //    Exists = collection.Exists,
            //    New = collection.New
            //};

            return View(/*viewModel*/);
        }

        [HttpPost]
        async public Task<IActionResult> Delete(PricingsPricingDetailsRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
