using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.FormModels
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [Compare("Password", ErrorMessage = "Şifrələr eyni olmalıdır!")]
        public string PasswordConfirm { get; set; }
    }
}
