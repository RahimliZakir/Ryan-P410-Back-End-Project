using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsModule
{
    public class PricingSingleQuery : IRequest<Pricing>
    {
        public int? Id { get; set; }

        public class PricingSingleQueryQueryHandler : IRequestHandler<PricingSingleQuery, Pricing>
        {
            readonly RyanDbContext db;

            public PricingSingleQueryQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Pricing> Handle(PricingSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Pricing? pricing = await db.Pricings.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return pricing;
            }
        }
    }
}
