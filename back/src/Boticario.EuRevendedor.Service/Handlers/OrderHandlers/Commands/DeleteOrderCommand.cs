using Boticario.EuRevendedor.Core.Extensions;
using Boticario.EuRevendedor.Core.Web;
using Flunt.Validations;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands
{
    public class DeleteOrderCommand : Request<Response>
    {
        public string Id { get; set; }

        public DeleteOrderCommand(string id)
        {
            AddNotifications(new Contract()
                  .Requires()
                  .IsNotNullOrWhiteSpace(id, nameof(id), nameof(id).Required())
              );

            if (Invalid)
                return;

            Id = id;
        }
    }
}
