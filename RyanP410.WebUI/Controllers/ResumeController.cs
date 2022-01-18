using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.CodingsModule;
using RyanP410.WebUI.AppCode.Modules.DesignModule;
using RyanP410.WebUI.AppCode.Modules.EducationsModule;
using RyanP410.WebUI.AppCode.Modules.ExperienceModule;
using RyanP410.WebUI.AppCode.Modules.KnowledgesModule;
using RyanP410.WebUI.AppCode.Modules.LanguagesModule;
using RyanP410.WebUI.AppCode.Modules.TestimonialsModule;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.Controllers
{
    [AllowAnonymous]
    public class ResumeController : Controller
    {
        readonly IMediator mediator;

        public ResumeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            ResumeViewModel viewModel = new ResumeViewModel();

            ExperiencesQuery experiencesQuery = new();
            IEnumerable<Experience> experiences = await mediator.Send(experiencesQuery);
            viewModel.Experiences = experiences;

            EducationsQuery educationQuery = new();
            IEnumerable<Education> educations = await mediator.Send(educationQuery);
            viewModel.Educations = educations;

            DesignsQuery designsQuery = new();
            IEnumerable<Design> designs = await mediator.Send(designsQuery);
            viewModel.Designs = designs;

            LanguagesQuery languagesQuery = new();
            IEnumerable<Language> languages = await mediator.Send(languagesQuery);
            viewModel.Languages = languages;

            CodingsQuery codingsQuery = new();
            IEnumerable<Coding> codings = await mediator.Send(codingsQuery);
            viewModel.Codings = codings;

            KnowledgesQuery knowledgesQuery = new();
            IEnumerable<Knowledge> knowledges = await mediator.Send(knowledgesQuery);
            viewModel.Knowledges = knowledges;

            TestimonialsQuery testimonialsQuery = new();
            IEnumerable<Testimonial> testimonials = await mediator.Send(testimonialsQuery);
            viewModel.Testimonials = testimonials;

            return View(viewModel);
        }
    }
}
