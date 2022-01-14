using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonEditCommand : PersonViewModel, IRequest<int>
    {
        public class PersonEditCommandHandler : IRequestHandler<PersonEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IHostEnvironment env;

            public PersonEditCommandHandler(RyanDbContext db, IActionContextAccessor ctx, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }

            async public Task<int> Handle(PersonEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null && request.Id <= 0)
                {
                    return 0;
                }

                Person? entity = await db.Persons.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    return 0;
                }

                string fullpath = null;
                string currentpath = null;

                if (request.File == null && !string.IsNullOrWhiteSpace(request.FileTemp))
                {
                    request.ImagePath = entity.ImagePath;
                }
                else if (request.File == null)
                {
                    currentpath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "persons", "img", entity.ImagePath);
                }
                else if (request.File != null)
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"person-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    fullpath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "persons", "img", filename);

                    using (FileStream fs = new(fullpath, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                    }

                    request.ImagePath = filename;
                }

                if (ctx.IsValid())
                {
                    try
                    {
                        entity.ImagePath = request.ImagePath;

                        if (System.IO.File.Exists(currentpath) && !string.IsNullOrWhiteSpace(currentpath))
                        {
                            System.IO.File.Delete(currentpath);
                        }

                        await db.SaveChangesAsync(cancellationToken);

                        return entity.Id;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (System.IO.File.Exists(fullpath) && !string.IsNullOrWhiteSpace(fullpath))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                    }
                }

                return 0;
            }
        }
    }
}
