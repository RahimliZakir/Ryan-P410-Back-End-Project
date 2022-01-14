﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.ClientsModule
{
    public class ClientEditCommand : ClientViewModel, IRequest<int>
    {
        public class ClientEditCommandHandler : IRequestHandler<ClientEditCommand, int>
        {
            readonly RyanDbContext db;
            readonly IHostEnvironment env;
            readonly IActionContextAccessor ctx;

            public ClientEditCommandHandler(RyanDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }

            public async Task<int> Handle(ClientEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null && request.Id <= 0)
                {
                    return 0;
                }

                Client? entity = await db.Clients.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

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
                    currentpath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "clients", entity.ImagePath);
                }
                else if (request.File != null)
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string filename = $"client-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    fullpath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "clients", filename);

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
