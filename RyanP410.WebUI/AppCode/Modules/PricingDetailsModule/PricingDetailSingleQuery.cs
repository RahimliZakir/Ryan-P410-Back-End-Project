using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingDetailsModule
{
    public class PricingDetailSingleQuery : IRequest<PricingDetail>
    {
        public int? Id { get; set; }

        public class PricingDetailSingleQueryHandler : IRequestHandler<PricingDetailSingleQuery, PricingDetail>
        {
            readonly RyanDbContext db;

            public PricingDetailSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PricingDetail> Handle(PricingDetailSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                PricingDetail? detail = await db.PricingDetails.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return detail;
            }
        }
    }
}
