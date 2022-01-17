using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsQuery : IRequest<IEnumerable<PricingsPricingDetailsCollection>>
    {
        public class PricingsPricingDetailsQueryHandler : IRequestHandler<PricingsPricingDetailsQuery, IEnumerable<PricingsPricingDetailsCollection>>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<PricingsPricingDetailsCollection>> Handle(PricingsPricingDetailsQuery request, CancellationToken cancellationToken)
            {
                return await db.PricingsPricingDetailsCollections.ToListAsync(cancellationToken);
            }
        }
    }
}
