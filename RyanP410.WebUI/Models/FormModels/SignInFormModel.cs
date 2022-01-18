using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Models.FormModels
{
    public class SignInFormModel
    {
        [Required(ErrorMessage = "Xahiş olunur istifadəçi adınızı və ya email-inizi daxil edin!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Xahiş olunur şifrənizi daxil edin!")]
        [MinLength(8, ErrorMessage = "Minimum 8 simvol daxil olunmalıdır!")]
        public string Password { get; set; }
    }
}
