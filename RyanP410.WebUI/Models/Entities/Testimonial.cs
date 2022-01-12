using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Testimonial : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Profession { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public string? ImagePath { get; set; }

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
