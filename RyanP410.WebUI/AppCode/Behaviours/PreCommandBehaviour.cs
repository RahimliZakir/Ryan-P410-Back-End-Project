using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore.Storage;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Behaviours
{
    //public class PreCommandBehaviour<T> : IRequestPreProcessor<T>
    //{
    //    readonly RyanDbContext db;

    //    public PreCommandBehaviour(RyanDbContext db)
    //    {
    //        this.db = db;
    //    }

    //    public async Task Process(T request, CancellationToken cancellationToken)
    //    {
    //        IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);
    //    }
    //}
}
