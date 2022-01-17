using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperienceCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Position { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Place { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int BeginYear { get; set; }

        public int? EndYear { get; set; }

        public class ExperienceCreateCommandHandler : IRequestHandler<ExperienceCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public ExperienceCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(ExperienceCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var experience = new Experience();
                    experience.Position = request.Position;
                    experience.Place = request.Place;
                    experience.Description = request.Description;
                    experience.BeginYear = request.BeginYear;
                    experience.EndYear = request.EndYear;

                    await db.Experiences.AddAsync(experience, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return experience.Id;
                }

                return 0;
            }
        }
    }
}
