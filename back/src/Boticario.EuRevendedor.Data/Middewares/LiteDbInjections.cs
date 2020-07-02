using Boticario.EuRevendedor.Data.LiteDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Boticario.EuRevendedor.Data.Middewares
{
    public static class LiteDbInjections
    {
        public static void InjectLiteDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = CheckConnectionString(configuration);
            services.Configure<LiteDbOptions>(options => {
                options.DatabaseLocation = connectionString;
            });

            services.AddSingleton<ILiteDbContext, LiteDbContext>();
        }

        private static string CheckConnectionString(IConfiguration configuration)
        {
            if (configuration == null)
            {
                return string.Empty;
            }

            var connectionString = configuration.GetSection("LiteDbOptions:DatabaseLocation").Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new FileNotFoundException();
            }

            var baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var pathToContentRoot = Path.Combine(baseDirectory, "Boticário", "EuRevendedor");
            var dbFolder = Path.Combine(pathToContentRoot, "db");

            if (!Directory.Exists(dbFolder))
            {
                Directory.CreateDirectory(dbFolder);
            }
            var dbPath = Path.Combine(dbFolder, connectionString);
            return dbPath;
        }
    }
}
