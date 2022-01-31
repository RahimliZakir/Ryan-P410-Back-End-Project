using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoriesUserSideQuery : IRequest<IEnumerable<BlogTagCategoryCollection>>
    {
        public class BlogTagCategoriesUserSideQueryHandler : IRequestHandler<BlogTagCategoriesUserSideQuery, IEnumerable<BlogTagCategoryCollection>>
        {
            readonly RyanDbContext db;

            public BlogTagCategoriesUserSideQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<BlogTagCategoryCollection>> Handle(BlogTagCategoriesUserSideQuery request, CancellationToken cancellationToken)
            {
                var data = await db.BlogTagCategoryCollections
                                   .Include(b => b.Tag)
                                   .Include(b => b.BlogCategory)
                                   .Include(b => b.Blog)
                                   .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
