using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.EducationsModule
{
    public class EducationRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class EducationRemoveCommandHandler : IRequestHandler<EducationRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public EducationRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(EducationRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                Education? entity = await db.Educations.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.Educations.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
