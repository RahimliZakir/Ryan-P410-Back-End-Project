using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoryCreateCommand : BlogCollectionFormModel, IRequest<JsonCommandResponse>
    {
        public class BlogTagCategoryCreateCommandHandler : IRequestHandler<BlogTagCategoryCreateCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogTagCategoryCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<JsonCommandResponse> Handle(BlogTagCategoryCreateCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                try
                {
                    foreach (var item in request.BlogTagCategoryCollections)
                    {
                        var collection = new BlogTagCategoryCollection
                        {
                            BlogId = request.Blog.Id,
                            BlogCategoryId = item.BlogCategory.Id,
                            TagId = item.Tag.Id,
                            CreatedByUserId = (int)ctx.GetUserId()
                        };

                        await db.BlogTagCategoryCollections.AddAsync(collection, cancellationToken);
                    }

                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Uğurla əlavə olundu!";
                }
                catch (Exception)
                {
                    response.Error = true;
                    response.Message = "Məlumatlar əlavə olunan zaman xəta baş verdi!";
                }

                return response;
            }
        }
    }
}
