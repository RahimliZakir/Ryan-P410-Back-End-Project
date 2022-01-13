using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.FunFactsModule
{
    public class FunFactSingleQuery : IRequest<FunFact>
    {
        public int? Id { get; set; }

        public class FunFactSingleQueryHandler : IRequestHandler<FunFactSingleQuery, FunFact>
        {
            readonly RyanDbContext db;

            public FunFactSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<FunFact> Handle(FunFactSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return await db.FunFacts.FirstOrDefaultAsync(cancellationToken); ;
                }

                FunFact? funFact = await db.FunFacts.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return funFact;
            }
        }
    }
}
