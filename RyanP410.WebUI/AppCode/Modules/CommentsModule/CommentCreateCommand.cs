using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.CommentsModule
{
    public class CommentCreateCommand : IRequest<Comment>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }

        public int BlogId { get; set; }

        public class CommentCreateCommandHandler : IRequestHandler<CommentCreateCommand, Comment>
        {
            readonly RyanDbContext db;
            readonly IActionContextAccessor ctx;

            public CommentCreateCommandHandler(RyanDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<Comment> Handle(CommentCreateCommand request, CancellationToken cancellationToken)
            {
                Comment comment = new();

                comment.Content = request.Content;
                comment.BlogId = request.BlogId;

                if ((request.ParentId ?? 0) > 0)
                {
                    comment.ParentId = request.ParentId;
                }

                int userId = (int)ctx.GetUserId();
                comment.UserId = userId;

                comment.User = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                await db.Comments.AddAsync(comment, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return comment;
            }
        }
    }
}
