using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.TestimonialsModule
{
    public class TestimonialCreateCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Profession { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public IFormFile? File { get; set; }

        public string? ImagePath { get; set; }

        public class TestimonialCreateCommandHandler : IRequestHandler<TestimonialCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IHostEnvironment env;
            readonly IActionContextAccessor ctx;

            public TestimonialCreateCommandHandler(RyanDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }

            async public Task<int> Handle(TestimonialCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null)
                {
                    ctx.AddModelError("", "Fayl seçilməyib!");
                }
                else
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"testimonial-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "testimonials", filename);

                    using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                    }


                    if (ctx.IsValid())
                    {
                        Testimonial testimonial = new Testimonial();

                        try
                        {
                            testimonial.ImagePath = filename;
                        }
                        catch (Exception)
                        {
                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.File.Delete(fullname);
                            }
                        }

                        testimonial.FullName = request.FullName;
                        testimonial.Profession = request.Profession;
                        testimonial.Content = request.Content;

                        await db.Testimonials.AddAsync(testimonial, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        return testimonial.Id;
                    }
                }

                return 0;
            }
        }
    }
}
