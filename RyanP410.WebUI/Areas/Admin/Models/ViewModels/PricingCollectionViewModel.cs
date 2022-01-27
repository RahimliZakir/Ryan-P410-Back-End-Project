using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Models.ViewModels
{
    public class PricingCollectionViewModel
    {
        public Pricing Pricing { get; set; }


        public PricingDetailInfoViewModel[] PricingDetailInfos { get; set; }

    }
}
