using Boticario.EuRevendedor.Data.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            if(configuration == null) {
                throw new ArgumentNullException("\n\n\n As Configurações não foram encontradas. \n\n\n");
            }

            if(configuration["Db:ConnectionString"] == null) {
                throw new ArgumentNullException("\n\n\n As Configurações para o Banco de Dados não foram encontradas.\n\n Verifique o appsettings.*.json\n\n\n");
            }

            services.AddSingleton(typeof(MongoDbContext), serviceProvider => 
                new MongoDbContext(configuration["Db:ConnectionString"], configuration["Db:Nome"]));
        }
    }
}
