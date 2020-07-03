using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Interfaces.Service;
using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.ResellerHandlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, Response>
    {
        private readonly IJwtService jwtService;
        private readonly IUserRepository userRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public LoginHandler(
            IJwtService jwtService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.jwtService = jwtService;
            this.userRepository = userRepository;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            User reseller = null;
            if (request.GrantType.Equals("password"))
            {
                reseller = await HandleResellerAuthentication(response, request);
            }
            else if (request.GrantType.Equals("refresh_token"))
            {
                reseller = await HandleRefreshToken(response, request);
            }

            if (response.Invalid || reseller == null)
            {
                return response;
            }

            await HandleJwt(response, reseller);

            return response;
        }

        private async Task HandleJwt(Response response, User reseller)
        {
            var jwt = jwtService.CreateJsonWebToken(reseller);
            await refreshTokenRepository.Save(jwt.RefreshToken);

            response.AddValue(new
            {
                access_token = jwt.AccessToken,
                refresh_token = jwt.RefreshToken.Token,
                token_type = jwt.TokenType,
                expires_in = jwt.ExpiresIn,
                id = reseller.Id,
                name = reseller.Name,
                role = reseller.Role,
                cpf = reseller.Cpf
            });
        }

        private async Task<User> HandleResellerAuthentication(Response response, LoginCommand request)
        {
            var reseller = await userRepository.GetByEmailAsync(request.Email);
            var password = new PasswordManager(request.Password);

            if (reseller == null || !reseller.Password.Equals(password))
            {
                response.AddNotification("E-mail ou Senha inválidos!");
            }

            return reseller;
        }

        private async Task<User> HandleRefreshToken(Response response, LoginCommand command)
        {
            var token = await refreshTokenRepository.Get(command.RefreshToken);
            if (token == null)
            {
                response.AddNotification("Login inválido!");
            }
            else if (token.Expiration < DateTime.UtcNow)
            {
                response.AddNotification("Login expirado!");
            }

            if (response.Invalid)
            {
                return null;
            }

            return await userRepository.GetByEmailAsync(token.Email);
        }

    }
}
