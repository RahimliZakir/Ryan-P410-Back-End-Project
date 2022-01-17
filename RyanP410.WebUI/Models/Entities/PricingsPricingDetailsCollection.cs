using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class PricingsPricingDetailsCollection : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingId { get; set; }

        public virtual Pricing Pricing { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingDetailId { get; set; }

        public virtual PricingDetail PricingDetail { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        [NotMapped]
        public PricingCollectionFormModel[] Items { get; set; }
    }
}
