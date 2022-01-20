using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule
{
    public class BlogCategoryViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;
    }
}
