using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Work : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        public string? ImagePath { get; set; }

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
