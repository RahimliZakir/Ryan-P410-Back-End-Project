using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Map { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string PhoneNumber { get; set; } = null!;

        public bool IsFreelance { get; set; }

        public class AppInfoCreateCommandHandler : IRequestHandler<AppInfoCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public AppInfoCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(AppInfoCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    AppInfo appInfo = new()
                    {
                        Map = request.Map,
                        Address = request.Address,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        IsFreelance = request.IsFreelance
                    };

                    await db.AppInfos.AddAsync(appInfo, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return appInfo.Id;
                }

                return 0;
            }
        }
    }
}
