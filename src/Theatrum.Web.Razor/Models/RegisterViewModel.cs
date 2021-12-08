using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Theatrum.Web.Razor.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Пошта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
