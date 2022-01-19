using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.AppCode.Modules.PersonalSideModule
{
    public class PersonalSideGetCurrentQuery : IRequest<RyanUser>
    {
        public class PersonalSideGetCurrentQueryHandler : IRequestHandler<PersonalSideGetCurrentQuery, RyanUser>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public PersonalSideGetCurrentQueryHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<RyanUser> Handle(PersonalSideGetCurrentQuery request, CancellationToken cancellationToken)
            {
                RyanUser currentUser = await db.Users.FirstOrDefaultAsync(u => u.Id == ctx.GetUserId());

                return currentUser;
            }
        }
    }
}
