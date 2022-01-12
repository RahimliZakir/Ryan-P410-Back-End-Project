using MediatR;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Modules.QuotesModule
{
    public class QuoteSingleQuery : IRequest<Quote>
    {
        public int? Id { get; set; }

        public class QuoteSingleQueryHandler : IRequestHandler<QuoteSingleQuery, Quote>
        {
            readonly RyanDbContext db;

            public QuoteSingleQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<Quote> Handle(QuoteSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return await db.Quotes.FirstOrDefaultAsync(cancellationToken); ;
                }

                Quote? quote = await db.Quotes.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return quote;
            }
        }
    }
}
