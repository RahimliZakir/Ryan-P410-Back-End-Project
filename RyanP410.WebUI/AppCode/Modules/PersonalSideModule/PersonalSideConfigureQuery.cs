using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.AppCode.Modules.PersonalSideModule
{
    public class PersonalSideConfigureQuery : PersonalSideRequestBody, IRequest<int>
    {
        public class PersonalSideConfigureQueryHandler : IRequestHandler<PersonalSideConfigureQuery, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IHostEnvironment env;

            public PersonalSideConfigureQueryHandler(RyanDbContext db, IActionContextAccessor ctx, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }

            public async Task<int> Handle(PersonalSideConfigureQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                {
                    return 0;
                }

                RyanUser entity = await db.Users.FirstOrDefaultAsync(u => u.Id == ctx.GetUserId(), cancellationToken);

                if (entity == null)
                {
                    return 0;
                }

                if (ctx.IsValid())
                {
                    string fullPath = null;
                    string currentPath = null;

                    if (entity.ImagePath != null || (entity.ImagePath == null && request.File != null))
                    {
                        if (request.File == null && !string.IsNullOrWhiteSpace(request.FileTemp))
                        {
                            request.ImagePath = entity.ImagePath;
                        }
                        else if (request.File == null)
                        {
                            currentPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "users", entity.ImagePath);
                        }
                        else if (request.File != null)
                        {
                            string ext = Path.GetExtension(request.File.FileName);
                            string fileName = $"user-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                            fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "users", fileName);

                            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                            {
                                await request.File.CopyToAsync(fs, cancellationToken);
                            }

                            request.ImagePath = fileName;
                        }
                    }

                    try
                    {
                        request.NormalizedUserName = request.UserName.ToUpper();
                        request.NormalizedEmail = request.Email.ToUpper();

                        entity.ImagePath = request.ImagePath;
                        entity.Name = request.Name;
                        entity.Surname = request.Surname;
                        entity.UserName = request.UserName;
                        entity.NormalizedUserName = request.NormalizedUserName;
                        entity.Email = request.Email;
                        entity.NormalizedEmail = request.NormalizedEmail;
                        entity.EmailConfirmed = request.EmailConfirmed;
                        entity.PasswordHash = request.PasswordHash;
                        entity.SecurityStamp = request.SecurityStamp;
                        entity.ConcurrencyStamp = request.ConcurrencyStamp;
                        entity.PhoneNumber = request.PhoneNumber;
                        entity.PhoneNumberConfirmed = request.PhoneNumberConfirmed;
                        entity.TwoFactorEnabled = request.TwoFactorEnabled;
                        entity.LockoutEnd = request.LockoutEnd;
                        entity.LockoutEnabled = request.LockoutEnabled;
                        entity.AccessFailedCount = request.AccessFailedCount;

                        await db.SaveChangesAsync(cancellationToken);

                        if (System.IO.File.Exists(currentPath) && !string.IsNullOrWhiteSpace(currentPath))
                        {
                            System.IO.File.Delete(currentPath);
                        }

                        return entity.Id;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (System.IO.File.Exists(fullPath) && !string.IsNullOrWhiteSpace(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                        return 0;
                    }
                }

                return 0;
            }
        }
    }
}
