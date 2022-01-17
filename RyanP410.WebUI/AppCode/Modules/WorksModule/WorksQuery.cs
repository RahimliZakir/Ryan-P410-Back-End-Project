using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.WorksModule
{
    public class WorksQuery : IRequest<IEnumerable<Work>>
    {
        public class WorksQueryHandler : IRequestHandler<WorksQuery, IEnumerable<Work>>
        {
            readonly RyanDbContext db;

            public WorksQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Work>> Handle(WorksQuery request, CancellationToken cancellationToken)
            {
                return await db.Works.ToListAsync(cancellationToken);
            }
        }
    }
}
