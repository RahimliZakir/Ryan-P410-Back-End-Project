using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.CodingsModule
{
    public class CodingCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int Percent { get; set; }

        public class CodingCreateCommandHandler : IRequestHandler<CodingCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public CodingCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(CodingCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var coding = new Coding();
                    coding.Name = request.Name;
                    coding.Percent = request.Percent;

                    await db.Codings.AddAsync(coding, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return coding.Id;
                }

                return 0;
            }
        }
    }
}
