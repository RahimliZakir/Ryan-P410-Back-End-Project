using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfosQuery : IRequest<IEnumerable<AppInfo>>
    {
        public class AppInfosQueryHandler : IRequestHandler<AppInfosQuery, IEnumerable<AppInfo>>
        {
            readonly RyanDbContext db;

            public AppInfosQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<AppInfo>> Handle(AppInfosQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<AppInfo> data = await db.AppInfos.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
