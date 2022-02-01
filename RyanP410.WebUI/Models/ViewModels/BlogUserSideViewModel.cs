using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.ViewModels
{
    public class BlogUserSideViewModel
    {
        public Blog Blog { get; set; }

        public Blog PrevBlog { get; set; }

        public Blog NextBlog { get; set; }
    }
}
