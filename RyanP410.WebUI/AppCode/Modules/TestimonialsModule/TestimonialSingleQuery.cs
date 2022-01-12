using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.TestimonialsModule
{
    public class TestimonialSingleQuery : IRequest<Testimonial>
    {
        public int? Id { get; set; }

        public class TestimonialSingleQueryHandler : IRequestHandler<TestimonialSingleQuery, Testimonial>
        {
            readonly RyanDbContext db;

            public TestimonialSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Testimonial> Handle(TestimonialSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return await db.Testimonials.FirstOrDefaultAsync(cancellationToken); ;
                }

                Testimonial? testimonial = await db.Testimonials.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return testimonial;
            }
        }
    }
}
