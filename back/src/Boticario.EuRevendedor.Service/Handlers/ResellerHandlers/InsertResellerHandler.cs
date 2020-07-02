using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Models.ViewModels;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.ResellerHandlers
{
    public class InsertResellerHandler : IRequestHandler<InsertResellerCommand, Response>
    {
        private readonly IUserRepository repository;

        public InsertResellerHandler(IUserRepository userRepository)
        {
            repository = userRepository;
        }

        public async Task<Response> Handle(InsertResellerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            if (request.Invalid)
            {
                response.AddNotifications(request.Notifications);
                return response;
            }

            var reseller = await repository.GetByEmailAsync(request.Email);
            if (reseller != null)
            {
                response.AddNotification("Já existe um Revendedor com esse e-mail!");
                return response;
            }

            reseller = new User(request.Name, request.Cpf, request.Email, request.Password, request.Role);

            await repository.InsertAsync(reseller);
            response.AddValue(new UserViewModel(reseller));
            return response;
        }
    }
}
