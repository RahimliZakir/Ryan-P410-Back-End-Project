using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class PricingsPricingDetailsRemoveCommandHandler : IRequestHandler<PricingsPricingDetailsRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public PricingsPricingDetailsRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(PricingsPricingDetailsRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                var entity = await db.PricingsPricingDetailsCollections
                                   .Where(pcc => pcc.PricingId.Equals(request.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                foreach (PricingsPricingDetailsCollection item in entity)
                {
                    db.PricingsPricingDetailsCollections.Remove(item);
                }

                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
