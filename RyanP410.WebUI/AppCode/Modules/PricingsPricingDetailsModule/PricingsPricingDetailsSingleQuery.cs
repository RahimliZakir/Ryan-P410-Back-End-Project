using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Areas.Admin.Models.ViewModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsSingleQuery : IRequest<PricingCollectionViewModel>
    {
        public int? Id { get; set; }

        public class PricingsPricingDetailsSingleQueryHandler : IRequestHandler<PricingsPricingDetailsSingleQuery, PricingCollectionViewModel>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PricingCollectionViewModel> Handle(PricingsPricingDetailsSingleQuery request, CancellationToken cancellationToken)
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
                    PricingCollectionViewModel viewModel = new PricingCollectionViewModel();
                    viewModel.Pricing = await db.Pricings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                    viewModel.PricingDetailInfos = await (from pd in db.PricingDetails
                                                          join ppdc in db.PricingsPricingDetailsCollections.Include(pcc => pcc.Pricing) on pd.Id equals ppdc.PricingDetailId
                                                          where ppdc.PricingId == request.Id
                                                          select new PricingDetailInfoViewModel
                                                          {
                                                              PricingDetail = pd,
                                                              Exists = ppdc.Exists,
                                                              New = ppdc.New
                                                          })
                                                          .ToArrayAsync(cancellationToken);

                    return viewModel;

                    // Left Join - Blackboard
                    //var left = db.PricingDetails.AsQueryable();
                    //var right = db.PricingsPricingDetailsCollections.Include(pcc => pcc.Pricing).Where(pcc => pcc.PricingId == request.Id);

                    //fm.Collections = await (from d in left
                    //                        join pcc in right
                    //                        on new { PricingDetailId = d.Id } equals new { pcc.PricingDetailId } into joinedDetails
                    //                        from joinedDetails_item in joinedDetails.DefaultIfEmpty()
                    //                        select new PricingsPricingDetailsCollection
                    //                        {
                    //                            Id = joinedDetails_item.PricingId,
                    //                            PricingDetail = d,
                    //                            Exists = joinedDetails_item.Exists,
                    //                            New = joinedDetails_item.New,
                    //                            Pricing = joinedDetails_item.Pricing
                    //                        })
                    //                        .ToListAsync(cancellationToken);

                    //var data = await (from pd in db.PricingDetails
                    //           join ppdc in db.PricingsPricingDetailsCollections.Include(pcc => pcc.Pricing) on pd.Id equals ppdc.PricingDetailId
                    //           where ppdc.PricingId==request.Id
                    //           group g by g)
                    //                        .ToListAsync(cancellationToken);
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
