using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoEditCommand : AppInfoViewModel, IRequest<int>
    {
        public class AppInfoEditCommandHandler : IRequestHandler<AppInfoEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public AppInfoEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<int> Handle(AppInfoEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var entity = await db.AppInfos.FirstOrDefaultAsync(_ => _.Id.Equals(request.Id));

                if (entity == null)
                    return 0;

                if (ctx.IsValid())
                {
                    entity.Map = request.Map;
                    entity.Address = request.Address;
                    entity.Email = request.Email;
                    entity.PhoneNumber = request.PhoneNumber;
                    entity.IsFreelance = request.IsFreelance;

                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
