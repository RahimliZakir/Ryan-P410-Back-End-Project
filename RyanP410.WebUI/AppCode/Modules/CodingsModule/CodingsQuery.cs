using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.CodingsModule
{
    public class CodingsQuery : IRequest<IEnumerable<Coding>>
    {
        public class CodingsQueryHandler : IRequestHandler<CodingsQuery, IEnumerable<Coding>>
        {
            readonly RyanDbContext db;

            public CodingsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Coding>> Handle(CodingsQuery request, CancellationToken cancellationToken)
            {
                return await db.Codings.ToListAsync(cancellationToken);
            }
        }
    }
}
