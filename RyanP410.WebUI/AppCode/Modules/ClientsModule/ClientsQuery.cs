using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ClientsModule
{
    public class ClientsQuery : IRequest<IEnumerable<Client>>
    {
        public class ClientsQueryHandler : IRequestHandler<ClientsQuery, IEnumerable<Client>>
        {
            readonly RyanDbContext db;

            public ClientsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Client>> Handle(ClientsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Client> data = await db.Clients.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
