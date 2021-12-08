using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatrum.Models.Models
{
    public class AppUserModel
    {
        public Guid Id { get; set; }
        [DisplayName("Ім'я")]
        public string UserName { get; set; }
        [DisplayName("Пошта")]
        public string Email { get; set; }
        [DisplayName("Дата реєстрації")]
        public DateTimeOffset RegistrationDate { get; set; }
        [DisplayName("Дата Народження")]
        public DateTimeOffset BirthdayDate { get; set; }

        [DisplayName("Старий пароль")]
        public string OldPassword { get; set; }

        [DisplayName("Новий пароль")]
        public string NewPassword { get; set; }

        [DisplayName("Новий пароль")]
        public string ConfirmNewPassword { get; set; }
    }
}
