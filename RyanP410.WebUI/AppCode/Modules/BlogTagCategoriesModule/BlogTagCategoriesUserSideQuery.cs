using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoriesUserSideQuery : IRequest<IEnumerable<Blog>>
    {
        public class BlogTagCategoriesUserSideQueryHandler : IRequestHandler<BlogTagCategoriesUserSideQuery, IEnumerable<Blog>>
        {
            readonly RyanDbContext db;

            public BlogTagCategoriesUserSideQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Blog>> Handle(BlogTagCategoriesUserSideQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Blogs
                                   .Include(b => b.BlogTagCategoryCollections)
                                   .ThenInclude(b => b.Tag)
                                   .Include(b => b.BlogTagCategoryCollections)
                                   .ThenInclude(b => b.BlogCategory)
                                   .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
