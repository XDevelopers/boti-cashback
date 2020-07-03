using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Models.ViewModels;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.ResellerHandlers
{
    public class GetResellersHandler : IRequestHandler<GetResellersQuery, Response>
    {
        private readonly IUserRepository repository;

        public GetResellersHandler(IUserRepository resellerRepository)
        {
            repository = resellerRepository;
        }

        public async Task<Response> Handle(GetResellersQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var resellers = await repository.GetItemsAsync();
            response.AddValue(resellers.Select(r => {
                return new UserViewModel(r);
            }));

            return response;
        }
    }
}
