using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingsModule
{
    public class PricingCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Per { get; set; } = null!;

        public class PricingCreateCommandHandler : IRequestHandler<PricingCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public PricingCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(PricingCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    Pricing pricing = new()
                    {
                        Title = request.Title,
                        Icon = request.Icon,
                        Price = request.Price,
                        Per = request.Per,
                    };

                    await db.Pricings.AddAsync(pricing, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return pricing.Id;
                }

                return 0;
            }
        }
    }
}
