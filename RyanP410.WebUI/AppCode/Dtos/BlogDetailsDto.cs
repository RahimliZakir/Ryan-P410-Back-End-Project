using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.AppCode.Dtos
{
    public class BlogDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string TagName { get; set; }

        public string BlogCategoryName { get; set; }

        public string Author { get; set; }

        public IEnumerable<BlogTagCategoryCollection> Collections { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int CommentCount
        {
            get
            {
                if (Comments == null)
                {
                    return 0;
                }

                return Comments.Count();
            }
        }
    }
}
