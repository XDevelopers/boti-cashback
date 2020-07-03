using Boticario.EuRevendedor.Api.Middlewares;
using Boticario.EuRevendedor.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boticario.EuRevendedor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                        //.AllowCredentials()
                        .AllowAnyMethod();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Inversion Of Controls (Services, Repository, MongoDb)
            services.AddApplicationServices(Configuration);

            // Injection Of Controls from API
            services.InjectApiServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Eu Revendedor API - V1");
                options.RoutePrefix = "swagger/ui";
            });
        }
    }
}
