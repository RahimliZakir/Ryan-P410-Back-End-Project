using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class PricingDetail : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<PricingsPricingDetailsCollection> Collections { get; set; }
    }
}
