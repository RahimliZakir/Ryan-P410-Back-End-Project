using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperienceRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class ExperienceRemoveCommandHandler : IRequestHandler<ExperienceRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public ExperienceRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(ExperienceRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                Experience? entity = await db.Experiences.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.Experiences.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
