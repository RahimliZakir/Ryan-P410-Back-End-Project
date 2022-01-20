using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule
{
    public class BlogCategoriesQuery : IRequest<IEnumerable<BlogCategory>>
    {
        public class BlogCategoriesQueryHandler : IRequestHandler<BlogCategoriesQuery, IEnumerable<BlogCategory>>
        {
            readonly RyanDbContext db;

            public BlogCategoriesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<BlogCategory>> Handle(BlogCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await db.BlogCategories.ToListAsync(cancellationToken);
            }
        }
    }
}
