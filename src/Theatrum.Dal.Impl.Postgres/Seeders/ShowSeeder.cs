using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class ShowSeeder
    {
        public static async Task SeedShows(TheatrumDbContext context)
        {
            var items = new List<Show>()
            {
                new Show()
                {
                    Id = new Guid("2beb74b6-b08c-443b-8020-8916d2cb41b0"),
                    Name = "Show0",
                    Description = "Description0",
                    AgeLimitation = 0,
                    TheatrumId = new Guid("b6c574fa-bad1-48cb-95b9-d0af9a5124dd"),
                    StartDate = DateTimeOffset.Now+ TimeSpan.FromDays(200),
                    EndDate = DateTimeOffset.Now + TimeSpan.FromDays(200)+ TimeSpan.FromHours(2),
                },
                new Show()
                {
                    Id = new Guid("f1aabfff-e53e-4913-b6a6-20350771fbd1"),
                    Name = "Show1",
                    Description = "Description1",
                    AgeLimitation = 0,
                    TheatrumId = new Guid("04fb1475-b982-4246-8386-d4ce583362f2"),
                    StartDate = DateTimeOffset.Now+ TimeSpan.FromDays(200),
                    EndDate = DateTimeOffset.Now + TimeSpan.FromDays(200)+ TimeSpan.FromHours(2),
                },
            };
            foreach (var item in items)
            {
                var existItem = await context.Shows.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (existItem == null)
                {
                    await context.Shows.AddAsync(item);
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
