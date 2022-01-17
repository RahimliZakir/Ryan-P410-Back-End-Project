using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperiencesQuery : IRequest<IEnumerable<Experience>>
    {
        public class ExperiencesQueryHandler : IRequestHandler<ExperiencesQuery, IEnumerable<Experience>>
        {
            readonly RyanDbContext db;

            public ExperiencesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Experience>> Handle(ExperiencesQuery request, CancellationToken cancellationToken)
            {
                return await db.Experiences.ToListAsync(cancellationToken);
            }
        }
    }
}
