using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.TagsModule
{
    public class TagRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class TagRemoveCommandHandler : IRequestHandler<TagRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public TagRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(TagRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                Tag? entity = await db.Tags.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.Tags.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
