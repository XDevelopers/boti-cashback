using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Interfaces.Models;
using Boticario.EuRevendedor.Interfaces.Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Boticario.EuRevendedor.Service
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfigurations jwtConfigurations;

        public JwtService(JwtConfigurations configurations)
        {
            jwtConfigurations = configurations;
        }

        public JsonWebToken CreateJsonWebToken(IUser user)
        {
            var identity = GetClaimsIdentity(user);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = jwtConfigurations.Issuer,
                Audience = jwtConfigurations.Audience,
                IssuedAt = jwtConfigurations.IssuedAt,
                NotBefore = jwtConfigurations.NotBefore,
                Expires = jwtConfigurations.AccessTokenExpiration,
                SigningCredentials = jwtConfigurations.SigningCredentials
            });

            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = CreateRefreshToken(user.Email),
                ExpiresIn = TimeSpan.FromMinutes(jwtConfigurations.ValidForMinutes).Ticks
            };
        }

        private RefreshToken CreateRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                Email = email,
                Expiration = jwtConfigurations.RefreshTokenExpiration
            };

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            refreshToken.Token = token.Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            return refreshToken;
        }

        private static ClaimsIdentity GetClaimsIdentity(IUser user)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                }
            );

            return identity;
        }
    }
}
