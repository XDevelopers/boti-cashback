using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.EuRevendedor.Service.Middlewares
{
    public static class ServiceInjections
    {
        public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<JwtConfigurations>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IBoticarioApiService, BoticarioApiService>();
        }
    }
}
