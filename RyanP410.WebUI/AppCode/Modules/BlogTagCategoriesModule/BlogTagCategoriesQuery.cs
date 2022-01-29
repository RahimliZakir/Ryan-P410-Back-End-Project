using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoriesQuery : IRequest<IEnumerable<Blog>>
    {
        public class BlogTagCategoriesQueryHandler : IRequestHandler<BlogTagCategoriesQuery, IEnumerable<Blog>>
        {
            readonly RyanDbContext db;

            public BlogTagCategoriesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Blog>> Handle(BlogTagCategoriesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Blog> blogs = await (from b in db.Blogs
                                                 join bc in db.BlogTagCategoryCollections
                                                 on b.Id equals bc.BlogId
                                                 select new Blog
                                                 {
                                                     Id = b.Id,
                                                     ImagePath = b.ImagePath,
                                                     Title = b.Title,
                                                     Description = b.Description
                                                 })
                                                 .Distinct()
                                                 .ToListAsync(cancellationToken);

                return blogs;
            }
        }
    }
}
