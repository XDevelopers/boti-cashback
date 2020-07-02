using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, Response>
    {
        private readonly IOrderRepository repository;

        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            repository = orderRepository;
        }

        public async Task<Response> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var orders = await repository.GetItemsAsync();
            response.AddValue(orders);

            return response;
        }
    }
}
