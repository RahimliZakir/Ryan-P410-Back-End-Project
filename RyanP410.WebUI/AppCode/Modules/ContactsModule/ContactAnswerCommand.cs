using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ContactsModule
{
    public class ContactAnswerCommand : AnswerContactFormModel, IRequest<JsonCommandResponse>
    {
        public class ContactAnswerCommandHandler : IRequestHandler<ContactAnswerCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IConfiguration conf;

            public ContactAnswerCommandHandler(RyanDbContext db, IActionContextAccessor ctx, IConfiguration conf)
            {
                this.db = db;
                this.ctx = ctx;
                this.conf = conf;
            }

            async public Task<JsonCommandResponse> Handle(ContactAnswerCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = false;
                    response.Message = "Məlumat tamlığı qorunmayıb!";

                    goto end;
                }

                Contact? entity = await db.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (!ctx.IsValid())
                {
                    response.Error = false;
                    response.Message = "Məlumatlar düzgün göndərilməyib!";

                    goto end;
                }

                if (ctx.IsValid())
                {
                    bool sentResponse = conf.SendMail(conf["FactoryCredentials:Email"], conf.GetValue<string>("FactoryCredentials:Psswrd"), request.EmailAddress, "Cavab - Ryan Team", $"<h3>Sizin sual</h3> <p>- {request.Message}</p> <h3>Bizim cavab</h3> <p>- {request.AnswerMessage}</p>", true);

                    if (sentResponse == false)
                    {

                        response.Error = true;
                        response.Message = "Mail göndərilən zaman xəta baş verdi, biraz sonra yenidən yoxlayın!";

                        goto end;
                    }
                    else
                    {
                        try
                        {
                            entity.AnswerMessage = request.AnswerMessage;
                            entity.AnswerDate = DateTime.UtcNow.AddHours(4);
                            await db.SaveChangesAsync(cancellationToken);


                            response.Error = false;
                            response.Message = "Müraciət uğurla cavablandırıldı və istifadəçinin Email qutusuna göndərildi!";

                            goto end;
                        }
                        catch (Exception)
                        {
                            response.Error = false;
                            response.Message = "Xəta baş verdi, biraz sonra yenidən yoxlayın!";

                            goto end;
                        }
                    }
                }

            end:
                return response;
            }
        }
    }
}
