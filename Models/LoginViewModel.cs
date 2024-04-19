using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ATLANT.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set;}

    }
}
