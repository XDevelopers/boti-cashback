using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Interfaces.Enumerators;
using Boticario.EuRevendedor.Repository.MongoDb;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Response>
    {
        private readonly IOrderRepository repository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            repository = orderRepository;
        }

        public async Task<Response> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
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
                response.AddNotification("Compras já aprovadas, não podem ser editadas!");
                return response;
            }

            var registeredOrder = await repository.GetByCode(request.Code);
            if (registeredOrder != null && registeredOrder.Code != order.Code)
            {
                response.AddNotification("Já existe uma Compra com esse código!");
                return response;
            }

            order.PrepareToUpdate(request.Code, request.Value, request.Date, request.Cpf);
            await repository.UpdateAsync(order);
            response.AddValue(order);

            return response;
        }
    }
}
