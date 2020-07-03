using Boticario.EuRevendedor.Api.Policies;
using Boticario.EuRevendedor.Core.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Boticario.EuRevendedor.Api.Extensions
{
    public static class MvcExtensions
    {
        public static void AddAuthorizedMvc(this IServiceCollection services)
        {
            AddJwtAuthorization(services);
            AddMvc(services);
        }

        private static void AddJwtAuthorization(IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetRequiredService<JwtConfigurations>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = settings.SigningCredentials.Key
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteUserPolicy", policy =>
                    policy.Requirements.Add(new DeleteUserRequirement("CanDeleteUser")));

                options.AddPolicy("Administrador",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Administrador");
                    });

                options.AddPolicy("Revendedor",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Revendedor");
                    });
            });

            services.AddSingleton<IAuthorizationHandler, DeleteUserRequirementHandler>();
        }

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
