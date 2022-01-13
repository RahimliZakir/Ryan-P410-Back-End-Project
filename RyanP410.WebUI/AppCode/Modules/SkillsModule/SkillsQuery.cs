using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.SkillsModule
{
    public class SkillsQuery : IRequest<IEnumerable<Skill>>
    {
        public class SkillsQueryHandler : IRequestHandler<SkillsQuery, IEnumerable<Skill>>
        {
            readonly RyanDbContext db;

            public SkillsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Skill>> Handle(SkillsQuery request, CancellationToken cancellationToken)
            {
                return await db.Skills.ToListAsync(cancellationToken);
            }
        }
    }
}
