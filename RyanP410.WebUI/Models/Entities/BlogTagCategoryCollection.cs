using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.Models.Entities
{
    public class BlogTagCategoryCollection : BaseEntity
    {
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public int BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual RyanUser CreatedByUser { get; set; }
    }
}
