using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class Comment : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Children { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public int UserId { get; set; }

        public virtual RyanUser User { get; set; }
    }
}
