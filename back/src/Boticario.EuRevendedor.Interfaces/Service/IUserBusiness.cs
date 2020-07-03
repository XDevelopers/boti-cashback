using Boticario.EuRevendedor.Interfaces.Models;

namespace Boticario.EuRevendedor.Interfaces.Service
{
    public interface IUserBusiness
    {
        IUser GetUser(string email, string password);
    }
}
