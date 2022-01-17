using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsSingleEditQuery : IRequest<PricingsPricingDetailsCollection>
    {
        public int? Id { get; set; }

        public class PricingsPricingDetailsSingleEditQueryHandler : IRequestHandler<PricingsPricingDetailsSingleEditQuery, PricingsPricingDetailsCollection>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsSingleEditQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PricingsPricingDetailsCollection> Handle(PricingsPricingDetailsSingleEditQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.PricingsPricingDetailsCollections.FirstOrDefaultAsync(_ => _.PricingId.Equals(request.Id), cancellationToken);
            }
        }
    }
}
