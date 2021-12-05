using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatrum.Entities.Entities;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public class TicketSeeder
    {
        public static async Task SeedTickets(TheatrumDbContext context)
        {
            var items = new List<Ticket>()
            {
                new Ticket()
                {
                    Id = new Guid("a8b9c40e-c6b1-4042-80bc-f0c10ab3c204"),
                    AppUserId = new Guid("011e7622-48ef-4522-bddc-f8464686daa1"),
                    IsChecked = true,
                    SecurityKey = "01234567890123456789",
                    SessionId = new Guid("a8b9c40e-c6b1-4042-80bc-f0c10ab3c204"),
                },
                new Ticket()
                {
                    Id = new Guid("91763dce-c7df-42a3-b5ca-aab538b90e97"),
                    AppUserId = new Guid("fb0b84f3-2c3d-474d-aede-5325e0a4e7bc"),
                    IsChecked = true,
                    SecurityKey = "12345678901234567890",
                    SessionId = new Guid("a8b9c40e-c6b1-4042-80bc-f0c10ab3c204"),
                },


            };
            foreach (var item in items)
            {
                var existItem = await context.Tickets.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (existItem == null)
                {
                    await context.Tickets.AddAsync(item);
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
