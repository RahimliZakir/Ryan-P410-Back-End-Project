using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Infrastructure
{
    public class PersonBase
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Residence { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Freelance { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Address { get; set; } = null!;
    }
}
