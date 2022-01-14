using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonCreateCommand : PersonBase, IRequest<int>
    {
        public IFormFile? File { get; set; }

        public IFormFile? Resume { get; set; }

        public class PersonCreateCommandHandler : IRequestHandler<PersonCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IHostEnvironment env;

            public PersonCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }

            async public Task<int> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null || request.Resume == null)
                {
                    ctx.AddModelError("", "Fayl seçilməyib!");
                }
                else
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"person-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "persons", "img", filename);

                    string cvExt = Path.GetExtension(request.Resume.FileName);
                    string cvFilename = $"resume{cvExt}";
                    string cvFullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "persons", "cv", cvFilename);

                    using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                        await request.Resume.CopyToAsync(fs);
                    }


                    if (ctx.IsValid())
                    {
                        Person person = new Person();

                        try
                        {
                            person.ImagePath = filename;
                            person.CvResumePath = cvFilename;
                        }
                        catch (Exception)
                        {
                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.File.Delete(fullname);
                            }

                            if (System.IO.File.Exists(cvFullname))
                            {
                                System.IO.File.Delete(cvFullname);
                            }
                        }

                        await db.Persons.AddAsync(person, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        return person.Id;
                    }
                }

                return 0;
            }
        }
    }
}
