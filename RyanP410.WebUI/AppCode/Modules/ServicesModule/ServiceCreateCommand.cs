using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.ServicesModule
{
    public class ServiceCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        public class ServiceCreateCommandHandler : IRequestHandler<ServiceCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public ServiceCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(ServiceCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var service = new Service();
                    service.Icon = request.Icon;
                    service.Title = request.Title;
                    service.Description = request.Description;

                    await db.Services.AddAsync(service, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return service.Id;
                }

                return 0;
            }
        }
    }
}
