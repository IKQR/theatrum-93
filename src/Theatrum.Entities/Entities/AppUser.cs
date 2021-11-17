using System;
using Microsoft.AspNetCore.Identity;

namespace Theatrum.Entities.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}
