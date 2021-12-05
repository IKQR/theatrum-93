using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class SessionSeeder
    {
        public static async Task SeedSessions(TheatrumDbContext context)
        {
            var items = new List<Session>()
            {
                new Session()
                {
                    Id = new Guid("a8b9c40e-c6b1-4042-80bc-f0c10ab3c204"),
                    StartDate = DateTimeOffset.Now + TimeSpan.FromDays(201),
                    ShowId = new Guid("2beb74b6-b08c-443b-8020-8916d2cb41b0"),
                },

                new Session()
                {
                    Id = new Guid("09cb9e01-80f1-4059-bbc5-6e16dabfe6bd"),
                    StartDate = DateTimeOffset.Now + TimeSpan.FromDays(207),
                    ShowId = new Guid("f1aabfff-e53e-4913-b6a6-20350771fbd1"),
                },

            };
            foreach (var item in items)
            {
                var existItem = await context.Sessions.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (existItem == null)
                {
                    await context.Sessions.AddAsync(item);
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
