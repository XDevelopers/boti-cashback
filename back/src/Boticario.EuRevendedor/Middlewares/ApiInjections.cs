using Boticario.EuRevendedor.Api.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Boticario.EuRevendedor.Api.Middlewares
{
    public static class ApiInjections
    {
        public static IServiceCollection InjectApiServices(this IServiceCollection services)
        {
            //services.AddCors();
            //services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            //{
            //    builder.AllowAnyHeader().AllowAnyOrigin().AllowCredentials().AllowAnyMethod();
            //}));
            services.AddMvc();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddHealthChecks();


            services.InjectApplicationServicesWeb();

            InjectSwagger(services);

            InjectCustomServices(services);

            return services;
        }

        public static void InjectApplicationServicesWeb(this IServiceCollection services)
        {
            // TODO: Passo 1 - Comentar a linha abaixo não será necessário estar LOGADO para criar o Primeiro Usuário
            // TODO: Passo 2 - Comentar [Authorize] na Controller de Reseller
            services.InjectUsuarioLogado();
            services.AddAuthorizedMvc();
            services.InjectMediatR();
        }

        private static void InjectUsuarioLogado(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }

        private static void InjectMediatR(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Boticario.EuRevendedor.Service");
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestsValidationMiddleware<,>));
            services.AddMediatR(assembly);
        }

        private static void InjectCustomServices(IServiceCollection services)
        {
            // Customized Boticário Service
            services.AddHttpClient("boticario", client =>
            {
                client.BaseAddress = new Uri("https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/");
            });
        }

        private static void InjectSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eu revendedor 'O Boticário' - API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
