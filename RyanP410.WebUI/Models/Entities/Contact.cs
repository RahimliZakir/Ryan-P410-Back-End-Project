using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.Entities
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string FullName { get; set; } = null!; 

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        [EmailAddress(ErrorMessage = "Xahiş olunur Email formatında daxil edin!")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Message { get; set; } = null!;

        public DateTime? AnswerDate { get; set; }   
          
        public string? AnswerMessage { get; set; }
    }
}
