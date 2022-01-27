using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Models.ViewModels
{
    public class PricingDetailInfoViewModel
    {
        public PricingDetail PricingDetail { get; set; }

        public bool Exists { get; set; }

        public bool New { get; set; }
    }
}
