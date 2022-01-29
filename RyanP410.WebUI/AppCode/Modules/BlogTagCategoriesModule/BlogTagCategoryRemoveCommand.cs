using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule
{
    public class BlogTagCategoryRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class BlogTagCategoriesRemoveCommandHandler : IRequestHandler<BlogTagCategoryRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public BlogTagCategoriesRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<JsonCommandResponse> Handle(BlogTagCategoryRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                var entity = await db.BlogTagCategoryCollections
                                   .Where(pcc => pcc.BlogId.Equals(request.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    db.BlogTagCategoryCollections.Remove(item);
                }

                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
