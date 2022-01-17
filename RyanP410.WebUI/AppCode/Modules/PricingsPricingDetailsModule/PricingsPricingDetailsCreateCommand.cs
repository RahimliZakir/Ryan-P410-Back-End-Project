using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsCreateCommand : IRequest<JsonCommandResponse>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingDetailId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public PricingCollectionFormModel[] Items { get; set; }

        public class PricingsPricingDetailsCreateCommandHandler : IRequestHandler<PricingsPricingDetailsCreateCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public PricingsPricingDetailsCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<JsonCommandResponse> Handle(PricingsPricingDetailsCreateCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new();

                if (ctx.IsValid())
                {
                    foreach (var item in request.Items)
                    {
                        PricingsPricingDetailsCollection collection = new();
                        collection.PricingId = request.PricingId;
                        collection.PricingDetailId = item.Id;
                        collection.Exists = item.Exists;
                        collection.New = item.New;
                        await db.PricingsPricingDetailsCollections.AddAsync(collection, cancellationToken);
                    }

                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Uğurla əlavə olundu!";
                    goto end;
                }

                response.Error = true;
                response.Message = "Əlavə olunan zaman xəta baş verdi!";
                goto end;

            end:
                return response;
            }
        }
    }
}
