using Microsoft.Extensions.DependencyInjection;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Bl.Impl.Services;

namespace Theatrum.Bl.Impl
{
    public static class BlDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //services
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<ITheatrService, TheatrService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IUserService, UserService>();

        }
    }
}
