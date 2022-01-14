using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonsQuery : IRequest<IEnumerable<Person>>
    {
        public class PersonsQueryHandler : IRequestHandler<PersonsQuery, IEnumerable<Person>>
        {
            readonly RyanDbContext db;

            public PersonsQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Person>> Handle(PersonsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Person> persons = await db.Persons.ToListAsync(cancellationToken);

                return persons;
            }
        }
    }
}
