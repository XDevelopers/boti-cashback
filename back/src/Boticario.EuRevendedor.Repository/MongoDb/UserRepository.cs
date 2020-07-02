using Boticario.EuRevendedor.Data.MongoDb;
using Boticario.EuRevendedor.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public class UserRepository : IUserRepository
    {
        protected MongoDbContext Context { get; }

        public UserRepository(MongoDbContext context)
        {
            Context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.GetAsync(Builders<User>.Filter.Eq(c => c.Email, email));
        }

        public async Task InsertAsync(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            user.CreatedAt = DateTimeOffset.UtcNow;

            await Context.InsertAsync(user);
        }
    }
}
