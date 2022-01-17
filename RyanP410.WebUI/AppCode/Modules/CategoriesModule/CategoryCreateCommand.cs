using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.CategoriesModule
{
    public class CategoryCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public CategoryCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var category = new Category();
                    category.Name = request.Name;

                    await db.Categories.AddAsync(category, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return category.Id;
                }

                return 0;
            }
        }
    }
}
