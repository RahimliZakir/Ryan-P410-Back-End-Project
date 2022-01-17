using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.EducationsModule
{
    public class EducationsQuery : IRequest<IEnumerable<Education>>
    {
        public class EducationsQueryHandler : IRequestHandler<EducationsQuery, IEnumerable<Education>>
        {
            readonly RyanDbContext db;

            public EducationsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Education>> Handle(EducationsQuery request, CancellationToken cancellationToken)
            {
                return await db.Educations.ToListAsync(cancellationToken);
            }
        }
    }
}
