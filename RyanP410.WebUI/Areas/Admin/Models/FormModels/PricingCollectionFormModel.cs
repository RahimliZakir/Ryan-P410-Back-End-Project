using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class PricingCollectionFormModel
    {
        public int Id { get; set; }

        public int PricingId { get; set; }

        public int PricingDetailsId { get; set; }

        public bool Exists { get; set; }

        public bool New { get; set; }

        public List<PricingsPricingDetailsCollection>? Collections { get; set; }
    }
}
