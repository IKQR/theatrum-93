using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Theatrum.Dal.Impl.Postgres;
using Theatrum.Dal.Impl.Postgres.Seeders;
using Theatrum.Entities.Entities;

namespace Theatrum.Web.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            IServiceScope scope = host.Services.CreateScope();
            Seed(scope);

            host.Run();
        }

        private static async Task Seed(IServiceScope scope)
        {
            try
            {
                //Migration
                TheatrumDbContext dbContext = scope.ServiceProvider.GetService<TheatrumDbContext>();
                await dbContext.Database.MigrateAsync();
                //Seeds
                //seed
                var seeder = scope.ServiceProvider.GetRequiredService<IDbContextSeeder<TheatrumDbContext>>();
                await seeder.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
