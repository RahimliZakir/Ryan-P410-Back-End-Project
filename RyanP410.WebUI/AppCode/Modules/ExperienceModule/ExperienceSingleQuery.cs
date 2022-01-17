using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperienceSingleQuery : IRequest<Experience>
    {
        public int? Id { get; set; }

        public class ExperienceSingleQueryHandler : IRequestHandler<ExperienceSingleQuery, Experience>
        {
            readonly RyanDbContext db;

            public ExperienceSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Experience> Handle(ExperienceSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Experiences.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
