using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.KnowledgesModule
{
    public class KnowledgeCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public class KnowledgeCreateCommandHandler : IRequestHandler<KnowledgeCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public KnowledgeCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(KnowledgeCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var knowledge = new Knowledge();
                    knowledge.Name = request.Name;

                    await db.Knowledges.AddAsync(knowledge, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return knowledge.Id;
                }

                return 0;
            }
        }
    }
}
