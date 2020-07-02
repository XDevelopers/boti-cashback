using Flunt.Notifications;
using MediatR;

namespace Boticario.EuRevendedor.Core.Web
{
    public abstract class Request<TResponse> : Notifiable, IRequest<TResponse>
    {
    }
}
