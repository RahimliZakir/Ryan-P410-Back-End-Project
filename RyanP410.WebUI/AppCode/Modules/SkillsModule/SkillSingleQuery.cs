using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.SkillsModule
{
    public class SkillSingleQuery : IRequest<Coding>
    {
        public int? Id { get; set; }

        public class SkillSingleQueryHandler : IRequestHandler<SkillSingleQuery, Coding>
        {
            readonly RyanDbContext db;

            public SkillSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Coding> Handle(SkillSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Skills.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
