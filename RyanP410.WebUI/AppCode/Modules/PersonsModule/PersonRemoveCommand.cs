using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class PersonRemoveCommandHandler : IRequestHandler<PersonRemoveCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;

            public PersonRemoveCommandHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<JsonCommandResponse> Handle(PersonRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                Person? entity = await db.Persons.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.Persons.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
