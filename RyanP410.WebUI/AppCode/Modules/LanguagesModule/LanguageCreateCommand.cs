using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.LanguagesModule
{
    public class LanguageCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int Percent { get; set; }

        public class LanguageCreateCommandHandler : IRequestHandler<LanguageCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public LanguageCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<int> Handle(LanguageCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    var language = new Language();
                    language.Name = request.Name;
                    language.Percent = request.Percent;

                    await db.Languages.AddAsync(language, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return language.Id;
                }

                return 0;
            }
        }
    }
}
