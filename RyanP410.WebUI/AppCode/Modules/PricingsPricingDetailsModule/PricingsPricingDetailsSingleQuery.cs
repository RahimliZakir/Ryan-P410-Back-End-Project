using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsSingleQuery : IRequest<PricingsPricingDetailsCollection>
    {
        public int? Id { get; set; }

        public class PricingsPricingDetailsSingleQueryHandler : IRequestHandler<PricingsPricingDetailsSingleQuery, PricingsPricingDetailsCollection>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PricingsPricingDetailsCollection> Handle(PricingsPricingDetailsSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                PricingsPricingDetailsCollection? collection = await db.PricingsPricingDetailsCollections
                                                                     .Include(pcp => pcp.Pricing)
                                                                     .Include(pcp => pcp.PricingDetail)
                                                                     .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return collection;
            }
        }
    }
}
