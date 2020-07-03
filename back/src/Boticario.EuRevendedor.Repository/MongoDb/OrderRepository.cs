using Boticario.EuRevendedor.Data.MongoDb;
using Boticario.EuRevendedor.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public class OrderRepository : IOrderRepository
    {
        protected MongoDbContext Context { get; }

        public OrderRepository(MongoDbContext context)
        {
            Context = context;
        }

        public async Task DeleteAsync(Order order)
        {
            await Context.DeleteAsync<Order>(order.Id);
        }
    

        public async Task<Order> GetByCode(string code)
        {
            return await Context.GetAsync(Builders<Order>.Filter.Eq(c => c.Code, code));
        }

        public async Task<Order> GetById(string id)
        {
            return await Context.GetByIdAsync<Order>(id);
        }

        public async Task<ICollection<Order>> GetItemsAsync()
        {
            return await Context.GetItemsAsync(Builders<Order>.Filter.Empty, 
                "Code", "Value", "Date", "Cpf", "AppliedCashback", "ValueCashback", "OrderStatus", "CreatedAt", "UpdatedAt");
        }

        public async Task<Order> InsertAsync(Order order)
        {
            order.Id = ObjectId.GenerateNewId().ToString();
            order.CreatedAt = DateTimeOffset.UtcNow;

            return await Context.InsertAsync(order);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            order.UpdatedAt = DateTimeOffset.UtcNow;

            return await Context.UpdateAsync(order.Id, order);
        }
    }
}
