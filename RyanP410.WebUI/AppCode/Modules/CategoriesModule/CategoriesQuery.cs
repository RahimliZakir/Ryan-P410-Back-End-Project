using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.CategoriesModule
{
    public class CategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public class CategoriesQueryHandler : IRequestHandler<CategoriesQuery, IEnumerable<Category>>
        {
            readonly RyanDbContext db;

            public CategoriesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Category>> Handle(CategoriesQuery request, CancellationToken cancellationToken)
            {
                return await db.Categories.ToListAsync(cancellationToken);
            }
        }
    }
}
