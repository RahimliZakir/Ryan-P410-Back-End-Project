using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoryEditCommand : BlogCollectionFormModel, IRequest<JsonCommandResponse>
    {
        public class BlogTagCategoryEditCommandHandler : IRequestHandler<BlogTagCategoryEditCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogTagCategoryEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<JsonCommandResponse> Handle(BlogTagCategoryEditCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Blog.Id == null || request.Blog.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                var entity = await db.BlogTagCategoryCollections
                                   .Where(p => p.BlogId.Equals(request.Blog.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir məlumat yoxdur!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    var coming = request.BlogTagCategoryCollections.FirstOrDefault(b => b.Tag.Id == item.TagId && b.BlogCategory.Id == item.BlogCategoryId);

                    if (coming == null)
                    {
                        var deleted = await db.BlogTagCategoryCollections
                                              .FirstOrDefaultAsync(b => b.BlogId == item.BlogId
                                              && b.TagId == item.TagId
                                              && b.BlogCategoryId == item.BlogCategoryId, cancellationToken);

                        db.BlogTagCategoryCollections.Remove(deleted);
                    }
                    else
                    {
                        item.BlogId = request.Blog.Id;
                        item.TagId = coming.Tag.Id;
                        item.BlogCategoryId = coming.BlogCategory.Id;
                    }
                }

                foreach (var item in request.BlogTagCategoryCollections.Where(b => !entity.Any(e => e.TagId == b.Tag.Id && e.BlogCategoryId == b.BlogCategory.Id)))
                {
                    var collection = new BlogTagCategoryCollection
                    {
                        BlogId = request.Blog.Id,
                        TagId = item.Tag.Id,
                        BlogCategoryId = item.BlogCategory.Id,
                        CreatedByUserId = (int)ctx.GetUserId()
                    };

                    await db.BlogTagCategoryCollections.AddAsync(collection, cancellationToken);
                }

                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Uğurla yeniləndi!";

            end:
                return response;
            }
        }
    }
}
