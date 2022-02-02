using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoryUserSideSingleQuery : IRequest<Blog>
    {
        public int? Id { get; set; }

        public class BlogTagCategoryUserSideSingleQueryHandler : IRequestHandler<BlogTagCategoryUserSideSingleQuery, Blog>
        {
            readonly RyanDbContext db;

            public BlogTagCategoryUserSideSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Blog> Handle(BlogTagCategoryUserSideSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return null;

                var data = await db.Blogs
                                   .Include(b => b.Comments)
                                   .ThenInclude(c => c.User)
                                   .Include(b => b.Comments)
                                   .ThenInclude(c => c.Children)
                                   .Include(b => b.BlogTagCategoryCollections)
                                   .ThenInclude(btc => btc.CreatedByUser)
                                   .Include(b => b.BlogTagCategoryCollections)
                                   .ThenInclude(btc => btc.Tag)
                                   .Include(b => b.BlogTagCategoryCollections)
                                   .ThenInclude(btc => btc.BlogCategory)
                                   .FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);

                return data;
            }
        }
    }
}
