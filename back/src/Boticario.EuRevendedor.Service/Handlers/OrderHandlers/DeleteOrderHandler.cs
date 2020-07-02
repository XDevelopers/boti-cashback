using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Interfaces.Enumerators;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Response>
    {
        private readonly IOrderRepository repository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            repository = orderRepository;
        }

        public async Task<Response> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            if (request.Invalid)
            {
                response.AddNotifications(request.Notifications);
                return response;
            }
            var order = await repository.GetById(request.Id);
            if (order == null)
            {
                response.AddNotification("Compra não encontrada!");
                return response;
            }

            if (order.OrderStatus == EnumOrderStatus.Approved)
            {
                response.AddNotification("Compras já aprovadas, não podem ser excluídas!");
                return response;
            }

            await repository.DeleteAsync(order);

            return response;
        }
    }
}
