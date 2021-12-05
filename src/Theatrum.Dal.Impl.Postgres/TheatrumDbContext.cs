using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres
{
    public class TheatrumDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public TheatrumDbContext(DbContextOptions<TheatrumDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Theatr> Theatrs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
