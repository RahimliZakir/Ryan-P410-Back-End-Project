using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.DesignModule
{
    public class DesignCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int Percent { get; set; }

        public class DesignCreateCommandHandler : IRequestHandler<DesignCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public DesignCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(DesignCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var design = new Design();
                    design.Name = request.Name;
                    design.Percent = request.Percent;

                    await db.Designs.AddAsync(design, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return design.Id;
                }

                return 0;
            }
        }
    }
}
