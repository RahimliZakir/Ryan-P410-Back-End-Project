using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.ContactsModule
{
    public class ContactSendMessageCommand : IRequest<JsonCommandResponse>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        [EmailAddress(ErrorMessage = "Xahiş olunur Email formatında daxil edin!")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Message { get; set; } = null!;

        public class ContactSendMessageCommandHandler : IRequestHandler<ContactSendMessageCommand, JsonCommandResponse>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public ContactSendMessageCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<JsonCommandResponse> Handle(ContactSendMessageCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";

                    goto end;
                }

                if (!request.EmailAddress.IsEmail())
                {
                    response.Error = true;
                    response.Message = "Xahiş olunur Email formatında daxil edin!";

                    goto end;
                }

                if (ctx.IsValid())
                {
                    try
                    {
                        Contact contact = new()
                        {
                            FullName = request.FullName,
                            EmailAddress = request.EmailAddress,
                            Message = request.Message
                        };

                        await db.Contacts.AddAsync(contact, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        response.Error = false;
                        response.Message = "Müraciətiniz uğurla bizə göndərildi!";

                        goto end;
                    }
                    catch (Exception)
                    {
                        response.Error = true;
                        response.Message = "Bilinməyən bir xəta baş verdi, bir neçə dəqiqə sonra yenidən yoxlayın!";

                        goto end;
                    }
                }

            end:
                return response;
            }
        }
    }
}
