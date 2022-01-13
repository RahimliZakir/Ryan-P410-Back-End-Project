using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.FunFactsModule
{
    public class FunFactCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public class FunFactCreateCommandHandler : IRequestHandler<FunFactCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public FunFactCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(FunFactCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    FunFact funFact = new()
                    {
                        Icon = request.Icon,
                        Name = request.Name
                    };

                    await db.FunFacts.AddAsync(funFact, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return funFact.Id;
                }

                return 0;
            }
        }
    }
}
