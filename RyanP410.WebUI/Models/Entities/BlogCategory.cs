using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class BlogCategory : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<BlogTagCategoryCollection> BlogTagCategoryCollections { get; set; }
    }
}
