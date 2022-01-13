using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.FunFactsModule
{
    public class FunFactsQuery : IRequest<IEnumerable<FunFact>>
    {
        public class FunFactsQueryHandler : IRequestHandler<FunFactsQuery, IEnumerable<FunFact>>
        {
            readonly RyanDbContext db;

            public FunFactsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<FunFact>> Handle(FunFactsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<FunFact> data = await db.FunFacts.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
