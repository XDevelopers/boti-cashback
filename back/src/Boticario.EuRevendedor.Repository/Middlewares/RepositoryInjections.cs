using Boticario.EuRevendedor.Repository.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.EuRevendedor.Repository.Middlewares
{
    public static class RepositoryInjections
    {
        public static void InjectRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            // Especial
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
