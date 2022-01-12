using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ContactsModule
{
    public class ContactsQuery : IRequest<IEnumerable<Contact>>
    {
        public class ContactsQueryHandler : IRequestHandler<ContactsQuery, IEnumerable<Contact>>
        {
            readonly RyanDbContext db;

            public ContactsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Contact>> Handle(ContactsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Contact> data = await db.Contacts.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
