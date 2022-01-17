using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.ExperienceModule
{
    public class ExperienceViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Position { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Place { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int BeginYear { get; set; }

        public int? EndYear { get; set; }
    }
}
