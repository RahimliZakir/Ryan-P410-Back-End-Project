using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.TagsModule
{
    public class TagsQuery : IRequest<IEnumerable<Tag>>
    {
        public class TagsQueryHandler : IRequestHandler<TagsQuery, IEnumerable<Tag>>
        {
            readonly RyanDbContext db;

            public TagsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Tag>> Handle(TagsQuery request, CancellationToken cancellationToken)
            {
                return await db.Tags.ToListAsync(cancellationToken);
            }
        }
    }
}
