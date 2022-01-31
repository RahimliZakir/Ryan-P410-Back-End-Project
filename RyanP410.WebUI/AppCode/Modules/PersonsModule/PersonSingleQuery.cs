using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonSingleQuery : IRequest<Person>
    {
        public int? Id { get; set; }

        public class PersonSingleQueryHandler : IRequestHandler<PersonSingleQuery, Person>
        {
            readonly RyanDbContext db;

            public PersonSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<Person> Handle(PersonSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return await db.Persons.FirstOrDefaultAsync(cancellationToken); ;

                Person? person = await db.Persons.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

                return person;
            }
        }
    }
}
