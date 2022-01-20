using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Modules.TagsModule
{
    public class TagEditCommand : TagViewModel, IRequest<int>
    {
        public class TagEditCommandHandler : IRequestHandler<TagEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public TagEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<int> Handle(TagEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var entity = await db.Tags.FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                    return 0;

                if (ctx.IsValid())
                {
                    entity.Name = request.Name;

                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
