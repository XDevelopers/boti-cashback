using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Interfaces.Models;

namespace Boticario.EuRevendedor.Interfaces.Service
{
    public interface IJwtService
    {
        JsonWebToken CreateJsonWebToken(IUser user);
    }
}
