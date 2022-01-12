using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class AnswerContactFormModel
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? EmailAddress { get; set; }

        public string? Message { get; set; }

        public DateTime AnswerDate { get; set; }

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string AnswerMessage { get; set; } = null!;
    }
}
