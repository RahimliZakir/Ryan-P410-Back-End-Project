using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ServicesModule
{
    public class ServiceSingleQuery : IRequest<Service>
    {
        public int? Id { get; set; }

        public class ServiceSingleQueryHandler : IRequestHandler<ServiceSingleQuery, Service>
        {
            readonly RyanDbContext db;

            public ServiceSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Service> Handle(ServiceSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Services.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
