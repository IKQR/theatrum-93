using System;
using Microsoft.AspNetCore.Identity;

namespace Theatrum.Entities.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public DateTimeOffset RegistrationDate { get; set; }
        public DateTimeOffset BirthdayDate { get; set; }
    }
}
