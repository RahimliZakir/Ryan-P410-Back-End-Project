using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Client : BaseEntity
    {
        public string? ImagePath { get; set; }

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
