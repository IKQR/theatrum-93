using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres
{
    public class TheatrumDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public TheatrumDbContext(DbContextOptions<TheatrumDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
