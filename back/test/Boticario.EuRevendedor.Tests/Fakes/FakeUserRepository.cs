using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Repository.MongoDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Tests.Fakes
{
    public class FakeUserRepository : IUserRepository
    {
        public List<User> users = new List<User>();

        public Task<User> GetByEmailAsync(string email)
        {
            User user = null;
            if (email == "teste@fake.com")
            {
                user = new User("test", "123456", "teste@fake.com", "123456", "Revendedor");
            }

            return Task.FromResult(user);
        }

        public Task InsertAsync(User user)
        {
            users.Add(user);
            return Task.CompletedTask;
        }
    }
}
