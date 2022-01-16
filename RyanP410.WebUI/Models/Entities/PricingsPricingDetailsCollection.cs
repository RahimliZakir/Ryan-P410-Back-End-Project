using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class PricingsPricingDetailsCollection : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingId { get; set; }

        public virtual Pricing Pricing { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingDetailId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public virtual PricingDetail PricingDetail { get; set; }
    }
}
