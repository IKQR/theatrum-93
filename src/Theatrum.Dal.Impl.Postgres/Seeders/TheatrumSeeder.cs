using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class TheatrumSeeder : IDbContextSeeder<TheatrumDbContext>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public TheatrumSeeder(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync(TheatrumDbContext context)
        {
            try
            {
                await AppUserSeeder.SeedAdmins(context, _userManager, _roleManager);
                await TheatrSeeder.SeedTheatrs(context);
                await ShowSeeder.SeedShows(context);
                await SessionSeeder.SeedSessions(context);
                await TicketSeeder.SeedTickets(context);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
