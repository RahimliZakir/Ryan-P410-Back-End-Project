using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.EducationsModule
{
    public class EducationCreateCommand : IRequest<int>
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

        public class EducationCreateCommandHandler : IRequestHandler<EducationCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public EducationCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(EducationCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var education = new Education();
                    education.Position = request.Position;
                    education.Place = request.Place;
                    education.Description = request.Description;
                    education.BeginYear = request.BeginYear;
                    education.EndYear = request.EndYear;

                    await db.Educations.AddAsync(education, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return education.Id;
                }

                return 0;
            }
        }
    }
}
