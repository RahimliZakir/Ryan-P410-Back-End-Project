using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.KnowledgesModule
{
    public class KnowledgeSingleQuery : IRequest<Knowledge>
    {
        public int? Id { get; set; }

        public class KnowledgeSingleQueryHandler : IRequestHandler<KnowledgeSingleQuery, Knowledge>
        {
            readonly RyanDbContext db;

            public KnowledgeSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Knowledge> Handle(KnowledgeSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Knowledges.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
