using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogsQuery : IRequest<IEnumerable<Blog>>
    {
        public class BlogsQueryHandler : IRequestHandler<BlogsQuery, IEnumerable<Blog>>
        {
            readonly RyanDbContext db;

            public BlogsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Blog>> Handle(BlogsQuery request, CancellationToken cancellationToken)
            {
                return await db.Blogs.ToListAsync(cancellationToken);
            }
        }
    }
}
