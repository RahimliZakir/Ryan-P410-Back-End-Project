using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.DesignModule
{
    public class DesignSingleQuery : IRequest<Design>
    {
        public int? Id { get; set; }

        public class DesignSingleQueryHandler : IRequestHandler<DesignSingleQuery, Design>
        {
            readonly RyanDbContext db;

            public DesignSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Design> Handle(DesignSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                return await db.Designs.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
