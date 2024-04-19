using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nickname")]
        public string? Nickname { get; set; }

        [Required]
        [Display(Name = "FIO")]
        public string? FIO { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }


    }
}
