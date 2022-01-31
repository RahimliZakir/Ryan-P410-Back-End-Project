using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Areas.Admin.Models.ViewModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsUserSideQuery : IRequest<IEnumerable<Pricing>>
    {
        public class PricingsPricingDetailsUserSideQueryHandler : IRequestHandler<PricingsPricingDetailsUserSideQuery, IEnumerable<Pricing>>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsUserSideQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Pricing>> Handle(PricingsPricingDetailsUserSideQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Pricings
                                   .Include(p => p.Collections)
                                   .ThenInclude(p => p.PricingDetail)
                                   .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
