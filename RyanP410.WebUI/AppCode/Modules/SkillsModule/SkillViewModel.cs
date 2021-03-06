using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.SkillsModule
{
    public class SkillViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string? Name { get; set; }
    }
}
