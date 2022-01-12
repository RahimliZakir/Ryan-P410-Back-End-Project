using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.QuotesModule
{
    public class QuotesQuery : IRequest<IEnumerable<Quote>>
    {
        public class QuotesQueryHandler : IRequestHandler<QuotesQuery, IEnumerable<Quote>>
        {
            readonly RyanDbContext db;

            public QuotesQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Quote>> Handle(QuotesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Quote> data = await db.Quotes.ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
