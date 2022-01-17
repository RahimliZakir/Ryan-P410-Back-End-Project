using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperienceEditCommand : ExperienceViewModel, IRequest<int>
    {
        public class ExperienceEditCommandHandler : IRequestHandler<ExperienceEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public ExperienceEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<int> Handle(ExperienceEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var entity = await db.Experiences.FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                    return 0;

                if (ctx.IsValid())
                {
                    entity.Position = request.Position;
                    entity.Place = request.Place;
                    entity.Description = request.Description;
                    entity.BeginYear = request.BeginYear;
                    entity.EndYear = request.EndYear;

                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
