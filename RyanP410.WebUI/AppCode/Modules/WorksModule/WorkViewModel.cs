using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.WorksModule
{
    public class WorkViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public int CategoryId { get; set; }

        public IFormFile? File { get; set; }

        public string? ImagePath { get; set; }

        public string? FileTemp { get; set; }
    }
}
