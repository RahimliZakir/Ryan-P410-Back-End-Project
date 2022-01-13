using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.FunFactsModule
{
    public class FunFactViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;
    }
}
