using RyanP410.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RyanP410.WebUI.Models.Entities
{
    public class Blog : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BlogTagCategoryCollection> BlogTagCategoryCollections { get; set; }

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
