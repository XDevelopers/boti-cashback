using Boticario.EuRevendedor.Data.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.EuRevendedor.Data.Middewares
{
    public static class MongoDbInjections
    {
        public static void InjectMongoDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoConnection(configuration);
        }

        private static void AddMongoConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(MongoDbContext), serviceProvider => 
                new MongoDbContext(configuration["Db:ConnectionString"], configuration["Db:Nome"]));
        }
    }
}
