using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.TagsModule
{
    public class TagCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public class TagCreateCommandHandler : IRequestHandler<TagCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public TagCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(TagCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var tag = new Tag();
                    tag.Name = request.Name;

                    await db.Tags.AddAsync(tag, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return tag.Id;
                }

                return 0;
            }
        }
    }
}
