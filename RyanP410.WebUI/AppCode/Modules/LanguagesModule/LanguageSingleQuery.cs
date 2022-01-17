using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.LanguagesModule
{
    public class LanguageSingleQuery : IRequest<Language>
    {
        public int? Id { get; set; }

        public class LanguageSingleQueryHandler : IRequestHandler<LanguageSingleQuery, Language>
        {
            readonly RyanDbContext db;

            public LanguageSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Language> Handle(LanguageSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Languages.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
