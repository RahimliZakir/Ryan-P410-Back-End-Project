using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingDetailsModule
{
    public class PricingDetailCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public class PricingDetailCreateCommandHandler : IRequestHandler<PricingDetailCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public PricingDetailCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(PricingDetailCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    PricingDetail detail = new()
                    {
                        Name = request.Name
                    };

                    await db.PricingDetails.AddAsync(detail, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return detail.Id;
                }

                return 0;
            }
        }
    }
}
