using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ContactsModule
{
    public class ContactSingleQuery : IRequest<Contact>
    {
        public int? Id { get; set; }

        public class ContactSingleQueryHandler : IRequestHandler<ContactSingleQuery, Contact>
        {
            readonly RyanDbContext db;

            public ContactSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<Contact> Handle(ContactSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return await db.Contacts.FirstOrDefaultAsync(cancellationToken);

                return await db.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            }
        }
    }
}
