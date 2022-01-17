using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.SkillsModule
{
    public class SkillCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public class SkillCreateCommandHandler : IRequestHandler<SkillCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public SkillCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(SkillCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var skill = new Coding();
                    skill.Name = request.Name;

                    await db.Skills.AddAsync(skill, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return skill.Id;
                }

                return 0;
            }
        }
    }
}
