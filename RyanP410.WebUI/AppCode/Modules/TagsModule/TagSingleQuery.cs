using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.TagsModule
{
    public class TagSingleQuery : IRequest<Tag>
    {
        public int? Id { get; set; }

        public class TagSingleQueryHandler : IRequestHandler<TagSingleQuery, Tag>
        {
            readonly RyanDbContext db;

            public TagSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Tag> Handle(TagSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Tags.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
