using RyanP410.WebUI.Models.FormModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Pricing : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Per { get; set; } = null!;

        public virtual ICollection<PricingsPricingDetailsCollection> Collections { get; set; }
    }
}