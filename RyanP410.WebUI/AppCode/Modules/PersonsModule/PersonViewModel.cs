using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PersonsModule
{
    public class PersonViewModel
    {
        public int? Id { get; set; }

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

        public string? ImagePath { get; set; }

        public string? CvResumePath { get; set; }

        public IFormFile? File { get; set; }

        public IFormFile? Resume { get; set; }

        public string? FileTemp { get; set; }
    }
}
