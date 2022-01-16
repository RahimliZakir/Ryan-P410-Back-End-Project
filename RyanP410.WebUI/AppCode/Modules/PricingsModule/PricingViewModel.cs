using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingsModule
{
    public class PricingViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Per { get; set; } = null!;
    }
}
