using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule
{
    public class BlogCategorySingleQuery : IRequest<BlogCategory>
    {
        public int? Id { get; set; }

        public class BlogCategorySingleQueryHandler : IRequestHandler<BlogCategorySingleQuery, BlogCategory>
        {
            readonly RyanDbContext db;

            public BlogCategorySingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogCategory> Handle(BlogCategorySingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return null;

                return await db.BlogCategories.FirstOrDefaultAsync(c => c.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
