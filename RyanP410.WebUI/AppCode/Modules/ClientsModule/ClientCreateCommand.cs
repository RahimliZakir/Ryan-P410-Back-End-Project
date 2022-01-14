using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ClientsModule
{
    public class ClientCreateCommand : IRequest<int>
    {
        public IFormFile? File { get; set; }

        public class ClientCreateCommandHandler : IRequestHandler<ClientCreateCommand, int>
        {
            readonly RyanDbContext db;
            readonly IHostEnvironment env;
            readonly IActionContextAccessor ctx;

            public ClientCreateCommandHandler(RyanDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }

            async public Task<int> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null)
                {
                    ctx.AddModelError("", "Fayl seçilməyib!");
                }
                else
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"client-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "clients", filename);

                    using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs);
                    }


                    if (ctx.IsValid())
                    {
                        Client client = new Client();

                        try
                        {
                            client.ImagePath = filename;
                        }
                        catch (Exception)
                        {
                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.File.Delete(fullname);
                            }
                        }

                        await db.Clients.AddAsync(client, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);

                        return client.Id;
                    }
                }

                return 0;
            }
        }
    }
}
