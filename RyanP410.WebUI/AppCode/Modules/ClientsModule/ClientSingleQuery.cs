using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ClientsModule
{
    public class ClientSingleQuery : IRequest<Client>
    {
        public int? Id { get; set; }

        public class ClientSingleQueryHandler : IRequestHandler<ClientSingleQuery, Client>
        {
            readonly RyanDbContext db;

            public ClientSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Client> Handle(ClientSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return await db.Clients.FirstOrDefaultAsync(cancellationToken); ;
                }

                Client? client = await db.Clients.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return client;
            }
        }
    }
}
