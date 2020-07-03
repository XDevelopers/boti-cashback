using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Boticario.EuRevendedor.Core.Security
{
    public class JwtConfigurations
    {
        public string Audience { get; }

        public string Issuer { get; }

        public int ValidForMinutes { get; }

        public int RefreshTokenValidForMinutes { get; }

        public SigningCredentials SigningCredentials { get; }

        public DateTime IssuedAt => DateTime.Now;

        public DateTime NotBefore => IssuedAt;

        public DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);

        public DateTime RefreshTokenExpiration => IssuedAt.AddMinutes(RefreshTokenValidForMinutes);

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtConfigurations"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public JwtConfigurations(IConfiguration configuration)
        {
            Issuer = configuration["JwtSettings:Issuer"];
            Audience = configuration["JwtSettings:Audience"];
            ValidForMinutes = Convert.ToInt32(configuration["JwtSettings:ValidForMinutes"]);
            RefreshTokenValidForMinutes = Convert.ToInt32(configuration["JwtSettings:RefreshTokenValidForMinutes"]);

            var signingKey = configuration["JwtSettings:SigningKey"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
