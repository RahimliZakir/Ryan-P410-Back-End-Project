using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.WorksModule
{
    public class WorkCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public int CategoryId { get; set; }

        public IFormFile? File { get; set; }

        public class WorkCreateCommandHandler : IRequestHandler<WorkCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IHostEnvironment env;
            readonly IActionContextAccessor ctx;

            public WorkCreateCommandHandler(RyanDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }

            async public Task<int> Handle(WorkCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null)
                {
                    ctx.AddModelError("", "Fayl seçilməyib!");
                }
                else
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"work-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "works", filename);

                    using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                    }


                    if (ctx.IsValid())
                    {
                        Work work = new Work();

                        try
                        {
                            work.ImagePath = filename;
                        }
                        catch (Exception)
                        {
                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.File.Delete(fullname);
                            }
                        }

                        work.Title = request.Title;
                        work.CategoryId = request.CategoryId;

                        await db.Works.AddAsync(work, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        return work.Id;
                    }
                }

                return 0;
            }
        }
    }
}
