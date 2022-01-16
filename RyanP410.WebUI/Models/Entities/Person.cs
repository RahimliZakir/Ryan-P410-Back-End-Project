using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Person : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Surname { get; set; } = null!;

        public string? ImagePath { get; set; }

        public string? CvResumePath { get; set; }

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

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
