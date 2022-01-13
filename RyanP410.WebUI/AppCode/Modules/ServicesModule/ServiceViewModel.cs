using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.ServicesModule
{
    public class ServiceViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;
    }
}
