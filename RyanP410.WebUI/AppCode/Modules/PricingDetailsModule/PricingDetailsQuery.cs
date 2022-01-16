using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingDetailsModule
{
    public class PricingDetailsQuery : IRequest<IEnumerable<PricingDetail>>
    {
        public class PricingDetailsQueryHandler : IRequestHandler<PricingDetailsQuery, IEnumerable<PricingDetail>>
        {
            readonly RyanDbContext db;

            public PricingDetailsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<PricingDetail>> Handle(PricingDetailsQuery request, CancellationToken cancellationToken)
            {
                return await db.PricingDetails.ToListAsync(cancellationToken);
            }
        }
    }
}
