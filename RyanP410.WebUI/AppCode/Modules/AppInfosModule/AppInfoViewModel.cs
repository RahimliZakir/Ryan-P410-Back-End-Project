using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Map { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string PhoneNumber { get; set; } = null!;

        public bool IsFreelance { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
