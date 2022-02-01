using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogSingleQuery : IRequest<Blog>
    {
        public int? Id { get; set; }

        public BlogSingleQuery() { }

        public BlogSingleQuery(int id)
        {
            this.Id = id;
        }

        public class BlogSingleQueryHandler : IRequestHandler<BlogSingleQuery, Blog>
        {
            readonly RyanDbContext db;

            public BlogSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<Blog> Handle(BlogSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return null;

                return await db.Blogs.FirstOrDefaultAsync(c => c.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
