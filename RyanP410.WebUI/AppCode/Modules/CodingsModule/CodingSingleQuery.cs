using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.CodingsModule
{
    public class CodingSingleQuery : IRequest<Coding>
    {
        public int? Id { get; set; }

        public class CodingSingleQueryHandler : IRequestHandler<CodingSingleQuery, Coding>
        {
            readonly RyanDbContext db;

            public CodingSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Coding> Handle(CodingSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Codings.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
