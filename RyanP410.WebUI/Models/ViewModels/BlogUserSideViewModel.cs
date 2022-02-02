using RyanP410.WebUI.AppCode.Dtos;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.ViewModels
{
    public class BlogUserSideViewModel
    {
        public BlogDetailsDto BlogDto { get; set; }

        public Blog PrevBlog { get; set; }

        public Blog NextBlog { get; set; }
    }
}
