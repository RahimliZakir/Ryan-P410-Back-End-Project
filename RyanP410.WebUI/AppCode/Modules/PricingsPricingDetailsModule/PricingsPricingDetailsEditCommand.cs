using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Areas.Admin.Models.ViewModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsEditCommand : PricingsPricingDetailsViewModel, IRequest<JsonCommandResponse>
    {
        public class PricingsPricingDetailsEditCommandHandler : IRequestHandler<PricingsPricingDetailsEditCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public PricingsPricingDetailsEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<JsonCommandResponse> Handle(PricingsPricingDetailsEditCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.PricingCollectionViewModel.Pricing.Id == null || request.PricingCollectionViewModel.Pricing.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                }

                var entity = await db.PricingsPricingDetailsCollections
                                   .Where(p => p.PricingId.Equals(request.PricingCollectionViewModel.Pricing.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir məlumat yoxdur!";
                }

                foreach (var item in entity)
                {
                    var coming = request.PricingCollectionViewModel.PricingDetailInfos.FirstOrDefault(p => p.PricingDetail.Id == item.PricingDetailId && p.Exists == item.Exists && p.New == item.New);

                    if (coming == null)
                    {
                        var deleted = await db.PricingsPricingDetailsCollections
                                              .FirstOrDefaultAsync(p =>
                                              p.PricingId == item.PricingId
                                              && p.PricingDetailId == item.PricingDetailId
                                              && p.Exists == item.Exists
                                              && p.New == item.New,
                                              cancellationToken);

                        db.PricingsPricingDetailsCollections.Remove(deleted);
                    }
                    else
                    {
                        item.PricingId = request.PricingCollectionViewModel.Pricing.Id;
                        item.PricingDetailId = coming.PricingDetail.Id;
                        item.New = coming.New;
                        item.Exists = coming.Exists;
                    }
                }

                foreach (var item in request.PricingCollectionViewModel.PricingDetailInfos.Where(pc => !entity.Any(e => e.PricingDetailId == pc.PricingDetail.Id && e.Exists == pc.Exists && e.New == pc.New)))
                {
                    PricingsPricingDetailsCollection collection = new()
                    {
                        PricingId = request.PricingCollectionViewModel.Pricing.Id,
                        PricingDetailId = item.PricingDetail.Id,
                        Exists = item.Exists,
                        New = item.New
                    };

                    await db.PricingsPricingDetailsCollections.AddAsync(collection, cancellationToken);
                }

                response.Error = false;
                response.Message = "Uğurla yeniləndi!";

                await db.SaveChangesAsync(cancellationToken);

                return response;
            }
        }
    }
}

