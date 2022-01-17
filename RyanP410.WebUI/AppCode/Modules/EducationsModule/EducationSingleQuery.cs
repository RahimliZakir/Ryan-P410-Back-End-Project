using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.EducationsModule
{
    public class EducationSingleQuery : IRequest<Education>
    {
        public int? Id { get; set; }

        public class EducationSingleQueryHandler : IRequestHandler<EducationSingleQuery, Education>
        {
            readonly RyanDbContext db;

            public EducationSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Education> Handle(EducationSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Educations.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
