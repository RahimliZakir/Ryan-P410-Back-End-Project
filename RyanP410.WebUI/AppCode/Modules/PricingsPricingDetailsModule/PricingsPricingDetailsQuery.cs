using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsQuery : IRequest<IEnumerable<Pricing>>
    {
        public class PricingsPricingDetailsQueryHandler : IRequestHandler<PricingsPricingDetailsQuery, IEnumerable<Pricing>>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Pricing>> Handle(PricingsPricingDetailsQuery request, CancellationToken cancellationToken)
            {

                var pricings = await (from p in db.Pricings
                                      join pcc in db.PricingsPricingDetailsCollections
                                      on p.Id equals pcc.PricingId
                                      select new Pricing
                                      {
                                          Id = p.Id,
                                          Icon = p.Icon,
                                          Title = p.Title
                                      })
                                      .Distinct()
                                      .ToListAsync(cancellationToken);

                return pricings;
            }
        }
    }
}
