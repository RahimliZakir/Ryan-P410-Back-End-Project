using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class Coding : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public int Percent { get; set; }
    }
}
