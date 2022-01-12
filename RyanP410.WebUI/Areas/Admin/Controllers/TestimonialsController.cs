#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.TestimonialsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialsController : Controller
    {
        readonly IMediator mediator;

        public TestimonialsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            TestimonialsQuery query = new TestimonialsQuery();

            IEnumerable<Testimonial> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(TestimonialSingleQuery query)
        {
            Testimonial testimonial = await mediator.Send(query);

            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Profession,Content,File")] TestimonialCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(TestimonialSingleQuery query)
        {
            var testimonial = await mediator.Send(query);

            if (testimonial == null)
            {
                return NotFound();
            }

            var vm = new TestimonialViewModel();

            vm.Id = testimonial.Id;
            vm.FullName = testimonial.FullName;
            vm.Profession = testimonial.Profession;
            vm.Content = testimonial.Content;
            vm.ImagePath = testimonial.ImagePath;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,Profession,Content,File,Id,FileTemp")] TestimonialEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TestimonialRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
