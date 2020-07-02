using Boticario.EuRevendedor.Service.Middlewares;
using Boticario.EuRevendedor.Data.Middewares;
using Boticario.EuRevendedor.Repository.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.EuRevendedor.IoC
{
    public static class Container
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.InjectMongoDbConnection(configuration);

            // Repository
            services.InjectRepository();
            
            // Business
            services.InjectServices(configuration);

            return services;
        }
    }
}
