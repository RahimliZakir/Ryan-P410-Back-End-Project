using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsSingleQuery : IRequest<PricingCollectionFormModel>
    {
        public int? Id { get; set; }

        public class PricingsPricingDetailsSingleQueryHandler : IRequestHandler<PricingsPricingDetailsSingleQuery, PricingCollectionFormModel>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PricingCollectionFormModel> Handle(PricingsPricingDetailsSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                PricingsPricingDetailsCollection? collection = await db.PricingsPricingDetailsCollections
                                                                       .Include(pcp => pcp.Pricing)
                                                                       .FirstOrDefaultAsync(m => m.PricingId == request.Id, cancellationToken);

                PricingCollectionFormModel fm = new PricingCollectionFormModel();

                fm.Collections = await (from d in db.PricingDetails
                                                     join pcc in db.PricingsPricingDetailsCollections.Where(pcc => pcc.PricingId == request.Id)
                                                     on new { PricingDetailId = d.Id } equals new { pcc.PricingDetailId } into joinedDetails
                                                     from joinedDetails_item in joinedDetails.DefaultIfEmpty()
                                                     select new PricingsPricingDetailsCollection
                                                     {
                                                         Id = collection.Id,
                                                         PricingDetail = d,
                                                         Exists = joinedDetails_item.Exists,
                                                         New = joinedDetails_item.New,
                                                         Pricing = collection.Pricing
                                                     }).ToListAsync(cancellationToken);

                return fm;
            }
        }
    }
}
