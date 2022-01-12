using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoSingleQuery : IRequest<AppInfo>
    {
        public int? Id { get; set; }

        public class AppInfoSingleQueryHandler : IRequestHandler<AppInfoSingleQuery, AppInfo>
        {
            readonly RyanDbContext db;

            public AppInfoSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<AppInfo> Handle(AppInfoSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return await db.AppInfos.FirstOrDefaultAsync(cancellationToken); ;
                }

                AppInfo? appInfo = await db.AppInfos.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return appInfo;
            }
        }
    }
}
