using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.TestimonialsModule
{
    public class TestimonialsQuery : IRequest<IEnumerable<Testimonial>>
    {
        public class TestimonialsQueryHandler : IRequestHandler<TestimonialsQuery, IEnumerable<Testimonial>>
        {
            readonly RyanDbContext db;

            public TestimonialsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Testimonial>> Handle(TestimonialsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Testimonial> data = await db.Testimonials.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
