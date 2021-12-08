using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Theatrum.Web.Razor.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Ім'я")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Пошта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Підтвердити пароль")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Пароль повторно")]
        [DataType(DataType.Date)]
        public DateTimeOffset Birthday { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}
