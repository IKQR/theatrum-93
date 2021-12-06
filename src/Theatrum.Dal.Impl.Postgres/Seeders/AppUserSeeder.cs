using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Theatrum.Entities.Entities;
using Theatrum.Utils;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class AppUserSeeder
    {
        public static async Task SeedAdmins(TheatrumDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var users = new List<Tuple<string, string, string, string>>()
            {

                new Tuple<string,string, string, string>("280f5c94-b053-4e59-817d-6ff1b1b18d23","admin@gmail.com","admin",Roles.Admin),
                new Tuple<string,string, string, string>("011e7622-48ef-4522-bddc-f8464686daa1","yarik@gmail.com","Qwerty.01",Roles.User),
                new Tuple<string,string, string, string>("fb0b84f3-2c3d-474d-aede-5325e0a4e7bc","balukovvva@gmail.com","Qwerty.01",Roles.User),
                new Tuple<string, string,string, string>("e8f53332-64c9-48ec-afa2-7b01304dc038","Illya0@gmail.com","Qwerty.01",Roles.User),
                new Tuple<string, string, string,string>("d3ceb96e-ebe6-4564-b688-e47029d99641","Illya1@gmail.com","Qwerty.01",Roles.User),
            };

            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new AppRole()
                {
                    Name = Roles.User
                });
            }
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new AppRole()
                {
                    Name = Roles.Admin
                });
            }
            foreach (var item in users)
            {
                if (await userManager.FindByNameAsync(item.Item2) == null)
                {
                    AppUser user = new AppUser
                    {
                        Id = new Guid(item.Item1),
                        Email = item.Item2,
                        UserName = item.Item2,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true,
                    };
                    IdentityResult result = await userManager.CreateAsync(user, item.Item3);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, item.Item4);
                        //await userManager.AddToRolesAsync(user, new List<string>()
                        //{
                        //    Roles.User,
                        //});
                    }
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
