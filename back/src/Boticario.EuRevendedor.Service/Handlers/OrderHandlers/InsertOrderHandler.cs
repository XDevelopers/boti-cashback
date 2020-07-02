using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, Response>
    {
        private readonly IOrderRepository repository;

        public InsertOrderHandler(IOrderRepository orderRepository)
        {
            repository = orderRepository;
        }

        public async Task<Response> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            if (request.Invalid)
            {
                response.AddNotifications(request.Notifications);
                return response;
            }

            var order = await repository.GetByCode(request.Code);
            if (order != null)
            {
                response.AddNotification("Já existe uma Compra com esse código!");
                return response;
            }

            order = new Order(request.Code, request.Value, request.Date, request.Cpf);
            await repository.InsertAsync(order);
            response.AddValue(order);
            return response;
        }
    }
}
