using RyanP410.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Work> Works { get; set; }
    }
}
