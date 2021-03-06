using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PricingDetailsModule
{
    public class PricingDetailRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class PricingDetailRemoveCommandHandler : IRequestHandler<PricingDetailRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public PricingDetailRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(PricingDetailRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                PricingDetail? entity = await db.PricingDetails.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.PricingDetails.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
