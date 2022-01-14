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
using RyanP410.WebUI.AppCode.Modules.PersonsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonsController : Controller
    {
        readonly IMediator mediator;

        public PersonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            PersonsQuery query = new PersonsQuery();

            IEnumerable<Person> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(PersonSingleQuery query)
        {
            Person person = await mediator.Send(query);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(PersonSingleQuery query)
        {
            var person = await mediator.Send(query);

            if (person == null)
            {
                return NotFound();
            }

            var vm = new PersonViewModel();

            vm.Id = person.Id;
            vm.Name = person.Name;
            vm.Surname = person.Surname;
            vm.ImagePath = person.ImagePath;
            vm.CvResumePath = person.CvResumePath;
            vm.Description = person.Description;
            vm.Age = person.Age;
            vm.Residence = person.Residence;
            vm.Freelance = person.Freelance;
            vm.Address = person.Address;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PersonRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
