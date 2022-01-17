using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class PricingCollectionFormModel
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }
    }
}
