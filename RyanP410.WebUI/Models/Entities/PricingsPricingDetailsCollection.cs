namespace RyanP410.WebUI.Models.Entities
{
    public class PricingsPricingDetailsCollection : BaseEntity
    {
        public int PricingId { get; set; }

        public virtual Pricing Pricing { get; set; }

        public int PricingDetailId { get; set; }

        public virtual PricingDetail PricingDetail { get; set; }
    }
}
