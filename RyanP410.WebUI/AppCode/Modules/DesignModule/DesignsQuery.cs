using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.DesignModule
{
    public class DesignsQuery : IRequest<IEnumerable<Design>>
    {
        public class DesignsQueryHandler : IRequestHandler<DesignsQuery, IEnumerable<Design>>
        {
            readonly RyanDbContext db;

            public DesignsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Design>> Handle(DesignsQuery request, CancellationToken cancellationToken)
            {
                return await db.Designs.ToListAsync(cancellationToken);
            }
        }
    }
}
