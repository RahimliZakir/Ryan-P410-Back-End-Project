using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int PricingDetailId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public PricingCollectionFormModel[] Items { get; set; }
    }
}
