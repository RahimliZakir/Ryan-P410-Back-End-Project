using MediatR.Pipeline;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Behaviours
{
    //public class PostCommandBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    //{
    //    readonly RyanDbContext db;

    //    public PostCommandBehaviour(RyanDbContext db)
    //    {
    //        this.db = db;
    //    }

    //    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    //    {
    //        if (db.Database.CurrentTransaction != null)
    //        {
    //            await db.Database.CurrentTransaction.CommitAsync(cancellationToken);
    //        }
    //    }
    //}
}
