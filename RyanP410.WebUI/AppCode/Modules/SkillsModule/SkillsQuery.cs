using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.SkillsModule
{
    public class SkillsQuery : IRequest<IEnumerable<Coding>>
    {
        public class SkillsQueryHandler : IRequestHandler<SkillsQuery, IEnumerable<Coding>>
        {
            readonly RyanDbContext db;

            public SkillsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Coding>> Handle(SkillsQuery request, CancellationToken cancellationToken)
            {
                return await db.Skills.ToListAsync(cancellationToken);
            }
        }
    }
}
