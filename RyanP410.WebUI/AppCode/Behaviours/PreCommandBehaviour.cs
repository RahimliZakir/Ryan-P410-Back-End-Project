using MediatR.Pipeline;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Behaviours
{
    public class PreCommandBehaviour<T> : IRequestPreProcessor<T>
        where T : notnull
    {
        readonly RyanDbContext db;

        public PreCommandBehaviour(RyanDbContext db)
        {
            this.db = db;
        }

        public async Task Process(T request, CancellationToken cancellationToken)
        {
            if (db.Database.CurrentTransaction != null)
                await db.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
