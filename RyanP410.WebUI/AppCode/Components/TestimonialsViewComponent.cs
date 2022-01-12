using MediatR;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.TestimonialsModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Components
{
    public class TestimonialsViewComponent : ViewComponent
    {
        readonly IMediator mediator;

        public TestimonialsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IViewComponentResult> InvokeAsync()
        {
            TestimonialsQuery query = new();

            IEnumerable<Testimonial> testimonials = await mediator.Send(query);

            return View(testimonials);
        }
    }
}
