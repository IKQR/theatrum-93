using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Theatrum.Dal.Impl.Postgres.Seeders
{
    public interface IDbContextSeeder<TContext>
        where TContext : DbContext
    {
        Task SeedAsync(TContext context);
    }
}
