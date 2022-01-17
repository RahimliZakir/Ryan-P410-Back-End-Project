using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.CategoriesModule
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Name { get; set; } = null!;
    }
}
