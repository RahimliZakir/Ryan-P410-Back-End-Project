using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class FunFact : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;
        public object Address { get; internal set; }
    }
}
