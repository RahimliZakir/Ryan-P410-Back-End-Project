using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;

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

                if (request.Id == null || request.Id <= 0)
                    response.Error = true;
                response.Message = "Məlumatın tamlığı qorunmayıb!";

                var entity = await db.PricingsPricingDetailsCollections.ToListAsync(cancellationToken);

                return response;
            }
        }
    }
}
