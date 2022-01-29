using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategorySingleQuery : IRequest<BlogCollectionFormModel>
    {
        public int? Id { get; set; }

        public class BlogTagCategorySingleQueryHandler : IRequestHandler<BlogTagCategorySingleQuery, BlogCollectionFormModel>
        {
            readonly RyanDbContext db;

            public BlogTagCategorySingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<BlogCollectionFormModel> Handle(BlogTagCategorySingleQuery request, CancellationToken cancellationToken)
            {
                BlogCollectionFormModel fm = new BlogCollectionFormModel();

                var blog = await db.Blogs.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
                fm.Blog = blog;

                var collection = await (from t in db.Tags
                                        join bc in db.BlogTagCategoryCollections on t.Id equals bc.TagId
                                        join c in db.BlogCategories on bc.BlogCategoryId equals c.Id
                                        where bc.Id == blog.Id
                                        select new BlogTagCategoryCollection
                                        {
                                            Tag = t,
                                            BlogCategory = c
                                        })
                                        .ToListAsync(cancellationToken);

                fm.BlogTagCategoryCollections = collection;

                return fm;
            }
        }
    }
}
