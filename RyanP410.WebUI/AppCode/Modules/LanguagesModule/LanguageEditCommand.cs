using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Modules.LanguagesModule
{
    public class LanguageEditCommand : LanguageViewModel, IRequest<int>
    {
        public class LanguageEditCommandHandler : IRequestHandler<LanguageEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public LanguageEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<int> Handle(LanguageEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var entity = await db.Languages.FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                    return 0;

                if (ctx.IsValid())
                {
                    entity.Name = request.Name;
                    entity.Percent = request.Percent;

                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
