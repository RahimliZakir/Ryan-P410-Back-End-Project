using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.FormModels
{
    public class PricingDetailItemFormModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }
    }
}
