using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public class BlogCreateCommandHandler : IRequestHandler<BlogCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IHostEnvironment env;

            public BlogCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }

            async public Task<int> Handle(BlogCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null)
                {
                    ctx.ActionContext.ModelState.AddModelError("", "Fayl seçilməyib!");
                }
                else
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"blog-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "blogs", filename);

                    using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                    }


                    if (ctx.IsValid())
                    {
                        Blog blog = new Blog();

                        try
                        {
                            blog.ImagePath = filename;
                        }
                        catch (Exception)
                        {
                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.File.Delete(fullname);
                            }
                        }

                        blog.Title = request.Title;
                        blog.Description = request.Description;

                        await db.Blogs.AddAsync(blog, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        return blog.Id;
                    }
                }

                return 0;
            }
        }
    }
}
