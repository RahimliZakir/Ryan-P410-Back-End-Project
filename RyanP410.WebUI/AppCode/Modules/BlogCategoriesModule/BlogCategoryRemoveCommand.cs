using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule
{
    public class BlogCategoryRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class BlogCategoryRemoveCommandHandler : IRequestHandler<BlogCategoryRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public BlogCategoryRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(BlogCategoryRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                BlogCategory? entity = await db.BlogCategories.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.BlogCategories.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
