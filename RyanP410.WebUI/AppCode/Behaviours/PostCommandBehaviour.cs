using MediatR;
using MediatR.Pipeline;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Behaviours
{
    public class PostCommandBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly RyanDbContext db;

        public PostCommandBehaviour(RyanDbContext db)
        {
            this.db = db;
        }

        async public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (db.Database.CurrentTransaction != null)
            {
                await db.Database.CurrentTransaction.CommitAsync(cancellationToken);
            }
        }
    }
}
