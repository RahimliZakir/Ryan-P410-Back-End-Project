using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities.Membership
{
    public class RyanUser : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped]
        public string? ImageTemp { get; set; }
    }
}
