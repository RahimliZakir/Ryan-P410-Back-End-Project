using RyanP410.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class Reply : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public int UserId { get; set; }

        public virtual RyanUser User { get; set; }
    }
}
