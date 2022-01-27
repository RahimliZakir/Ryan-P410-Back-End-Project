using RyanP410.WebUI.Areas.Admin.Models.ViewModels;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsViewModel
    {
        public int PricingId { get; set; }

        public int PricingDetailsId { get; set; }

        public bool Exists { get; set; }

        public bool New { get; set; }

        public PricingCollectionViewModel PricingCollectionViewModel { get; set; }
    }
}
