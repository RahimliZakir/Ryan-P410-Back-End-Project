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
                fm.Id = collection.Id;

                fm.Pricing = collection.Pricing;

                fm.PricingDetails = await (from d in db.PricingDetails
                                           join pcc in db.PricingsPricingDetailsCollections.Where(pcc => pcc.Id == request.Id)
                                           on new { PricingDetailId = d.Id } equals new { pcc.PricingDetailId } into joinedDetails
                                           from joinedDetails_item in joinedDetails.DefaultIfEmpty()
                                           select Tuple.Create(d, joinedDetails_item != null)).ToListAsync(cancellationToken);

                fm.Collection = await db.PricingsPricingDetailsCollections.Where(pcc => pcc.PricingId.Equals(collection.PricingId)).ToListAsync(cancellationToken);

                return fm;
            }
        }
    }
}
