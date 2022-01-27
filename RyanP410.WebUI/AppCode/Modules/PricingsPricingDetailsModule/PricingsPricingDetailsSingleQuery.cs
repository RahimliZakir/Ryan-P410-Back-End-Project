﻿using MediatR;
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

                //Other Way
                //var x = await db.Pricings.Include(p => p.Collections)
                //              .ThenInclude(p => p.PricingDetail)
                //              .Where(m => m.Id == request.Id)
                //              .ToListAsync(cancellationToken);


                //PricingsPricingDetailsCollection? collection = await db.PricingsPricingDetailsCollections
                //                                                       .Include(pcp => pcp.Pricing)
                //                                                       .FirstOrDefaultAsync(m => m.PricingId == request.Id, cancellationToken);

                try
                {
                    PricingCollectionFormModel fm = new PricingCollectionFormModel();

                    var left = db.PricingDetails.AsQueryable();
                    var right = db.PricingsPricingDetailsCollections.Include(pcc => pcc.Pricing).Where(pcc => pcc.PricingId == request.Id);

                    fm.Collections = await (from d in left
                                            join pcc in right
                                            on new { PricingDetailId = d.Id } equals new { pcc.PricingDetailId } into joinedDetails
                                            from joinedDetails_item in joinedDetails.DefaultIfEmpty()
                                            select new PricingsPricingDetailsCollection
                                            {
                                                Id = joinedDetails_item.PricingId,
                                                PricingDetail = d,
                                                Exists = joinedDetails_item.Exists,
                                                New = joinedDetails_item.New,
                                                Pricing = joinedDetails_item.Pricing
                                            })
                                            .ToListAsync(cancellationToken);

                    return fm;
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException;

                    throw;
                }
            }
        }
    }
}
