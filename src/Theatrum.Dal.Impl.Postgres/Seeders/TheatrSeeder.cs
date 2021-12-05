using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class TheatrSeeder
    {
        public static async Task SeedTheatrs(TheatrumDbContext context)
        {
            var items = new List<Theatr>()
            {
                new Theatr()
                {
                    Id = new Guid("b6c574fa-bad1-48cb-95b9-d0af9a5124dd"),
                    Name = "Theatr0",
                    Address = "Address0",
                    City = "City0",
                    Description = "Description0",
                },
                new Theatr()
                {
                    Id = new Guid("04fb1475-b982-4246-8386-d4ce583362f2"),
                    Name = "Theatr1",
                    Address = "Address1",
                    City = "City1",
                    Description = "Description1",
                }
               
            };
            foreach (var item in items)
            {
                var existItem = await context.Theatrs.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (existItem == null)
                {
                    await context.Theatrs.AddAsync(item);
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
