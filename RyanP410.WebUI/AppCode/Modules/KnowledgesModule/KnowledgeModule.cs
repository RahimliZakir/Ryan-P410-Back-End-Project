using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.KnowledgesModule
{
    public class KnowledgeModule : IRequest<IEnumerable<Knowledge>>
    {
        public class KnowledgeModuleHandler : IRequestHandler<KnowledgeModule, IEnumerable<Knowledge>>
        {
            readonly RyanDbContext db;

            public KnowledgeModuleHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Knowledge>> Handle(KnowledgeModule request, CancellationToken cancellationToken)
            {
                return await db.Knowledges.ToListAsync(cancellationToken);
            }
        }
    }
}
