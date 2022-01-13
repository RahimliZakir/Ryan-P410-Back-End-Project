using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ServicesModule
{
    public class ServicesQuery : IRequest<IEnumerable<Service>>
    {
        public class ServicesQueryHandler : IRequestHandler<ServicesQuery, IEnumerable<Service>>
        {
            readonly RyanDbContext db;

            public ServicesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Service>> Handle(ServicesQuery request, CancellationToken cancellationToken)
            {
                return await db.Services.ToListAsync(cancellationToken);
            }
        }
    }
}
