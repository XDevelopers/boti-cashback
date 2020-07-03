using Boticario.EuRevendedor.Data.MongoDb;
using Boticario.EuRevendedor.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task<User> GetById(string id)
        {
            return await Context.GetByIdAsync<User>(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.GetAsync(Builders<User>.Filter.Eq(c => c.Email, email));
        }

        public async Task<User> GetByCpf(string cpf)
        {
            return await Context.GetAsync(Builders<User>.Filter.Eq(c => c.Cpf, cpf));
        }

        public async Task<ICollection<User>> GetItemsAsync()
        {
            return await Context.GetItemsAsync(Builders<User>.Filter.Empty,
                "Name", "Cpf", "Email", "Role", "CreatedAt", "UpdatedAt");
        }

        public async Task InsertAsync(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            user.CreatedAt = DateTimeOffset.UtcNow;

            await Context.InsertAsync(user);
        }
    }
}
