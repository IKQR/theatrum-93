using Microsoft.Extensions.DependencyInjection;
using Theatrum.Dal.Abstract.IRepository;
using Theatrum.Dal.Impl.Postgres.Repository;

namespace Theatrum.Dal.Impl.Postgres
{
    public static class DalDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<ITheatrRepository, TheatrRepository>();
            services.AddTransient<ITheatrRepository, TheatrRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
        }
    }
}
