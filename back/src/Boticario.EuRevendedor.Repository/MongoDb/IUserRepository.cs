using Boticario.EuRevendedor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public interface IUserRepository
    {
        Task InsertAsync(User user);

        Task<User> GetByEmailAsync(string email);

        Task<User> GetByCpf(string cpf);

        Task<User> GetById(string id);

        Task<ICollection<User>> GetItemsAsync();
    }
}
