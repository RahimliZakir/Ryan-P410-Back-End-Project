using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class PricingCollectionFormModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;
    }
}
