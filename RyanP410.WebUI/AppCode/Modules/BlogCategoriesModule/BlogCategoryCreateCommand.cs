using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule
{
    public class BlogCategoryCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public class BlogCategoryCreateCommandHandler : IRequestHandler<BlogCategoryCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogCategoryCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(BlogCategoryCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var blogCategory = new BlogCategory();
                    blogCategory.Name = request.Name;

                    await db.BlogCategories.AddAsync(blogCategory, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return blogCategory.Id;
                }

                return 0;
            }
        }
    }
}
