using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.LanguagesModule
{
    public class LanguagesQuery : IRequest<IEnumerable<Language>>
    {
        public class LanguagesQueryHandler : IRequestHandler<LanguagesQuery, IEnumerable<Language>>
        {
            readonly RyanDbContext db;

            public LanguagesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Language>> Handle(LanguagesQuery request, CancellationToken cancellationToken)
            {
                return await db.Languages.ToListAsync(cancellationToken);
            }
        }
    }
}
