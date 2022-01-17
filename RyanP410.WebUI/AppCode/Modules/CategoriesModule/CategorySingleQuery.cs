using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.CategoriesModule
{
    public class CategorySingleQuery : IRequest<Category>
    {
        public int? Id { get; set; }

        public class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
        {
            readonly RyanDbContext db;

            public CategorySingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return null;

                return await db.Categories.FirstOrDefaultAsync(c => c.Id.Equals(request.Id), cancellationToken);
            }
        }
    }
}
