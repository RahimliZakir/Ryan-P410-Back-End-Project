using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.WorksModule
{
    public class WorkSingleQuery : IRequest<Work>
    {
        public int? Id { get; set; }

        public class WorkSingleQueryHandler : IRequestHandler<WorkSingleQuery, Work>
        {
            readonly RyanDbContext db;

            public WorkSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Work> Handle(WorkSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Works.Include(c => c.Category).FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
