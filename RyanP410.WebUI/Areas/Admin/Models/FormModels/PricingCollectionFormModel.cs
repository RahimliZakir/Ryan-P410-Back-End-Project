using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class PricingCollectionFormModel
    {
        public int Id { get; set; }

        public int PricingId { get; set; }

        public int PricingDetailsId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public List<PricingsPricingDetailsCollection>? Collections { get; set; }
    }

    public class PricingDetails
    {
        public Pricing Pricing { get; set; }

        public PricingDetail[] Detailss { get; set; }
    }
}
