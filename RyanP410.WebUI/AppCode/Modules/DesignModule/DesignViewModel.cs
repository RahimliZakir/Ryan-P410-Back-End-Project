using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.DesignModule
{
    public class DesignViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int Percent { get; set; }
    }
}
