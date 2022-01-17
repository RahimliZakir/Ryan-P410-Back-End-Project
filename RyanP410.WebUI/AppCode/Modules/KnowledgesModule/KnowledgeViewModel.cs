using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.KnowledgesModule
{
    public class KnowledgeViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Name { get; set; } = null!;
    }
}
