using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Repository.MongoDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Tests.Fakes
{
    public class FakeUserRepository : IUserRepository
    {
        public List<User> users = new List<User>();

        public async Task<User> GetByCpf(string cpf)
        {
            await Task.CompletedTask;
            return users.FirstOrDefault(c => c.Cpf == cpf);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            User user = null;
            if (email == "teste@fake.com")
            {
                user = new User("test", "123456", "teste@fake.com", "123456", "Revendedor");
            }

            return Task.FromResult(user);
        }

        public async Task<User> GetById(string id)
        {
            await Task.CompletedTask;
            return users.FirstOrDefault(c => c.Id == id);
        }

        public async Task<ICollection<User>> GetItemsAsync()
        {
            await Task.CompletedTask;
            return users;
        }

        public Task InsertAsync(User user)
        {
            users.Add(user);
            return Task.CompletedTask;
        }
    }
}
