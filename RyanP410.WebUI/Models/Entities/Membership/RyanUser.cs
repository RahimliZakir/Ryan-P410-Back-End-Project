using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities.Membership
{
    public class RyanUser : IdentityUser<int>
    {
        [Required(ErrorMessage = "Bu hissə doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə doldurulmalıdır!")]
        public string Surname { get; set; } = null!;

        public string? ImagePath { get; set; }

        [NotMapped]
        public string? ImageTemp { get; set; }
    }
}
