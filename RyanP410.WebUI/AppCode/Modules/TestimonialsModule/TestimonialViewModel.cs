using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.TestimonialsModule
{
    public class TestimonialViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Profession { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public IFormFile? File { get; set; }

        public string? ImagePath { get; set; }

        public string? FileTemp { get; set; }
    }
}
