using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsModule
{
    public class PricingsQuery : IRequest<IEnumerable<Pricing>>
    {
        public class PricingsQueryHandler : IRequestHandler<PricingsQuery, IEnumerable<Pricing>>
        {
            readonly RyanDbContext db;

            public PricingsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Pricing>> Handle(PricingsQuery request, CancellationToken cancellationToken)
            {
                return await db.Pricings.ToListAsync(cancellationToken);
            }
        }
    }
}
