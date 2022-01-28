using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public string? FileTemp { get; set; }
    }
}
