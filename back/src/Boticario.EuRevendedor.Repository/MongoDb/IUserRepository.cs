using Boticario.EuRevendedor.Models;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public interface IUserRepository
    {
        Task InsertAsync(User user);

        Task<User> GetByEmailAsync(string email);
    }
}
