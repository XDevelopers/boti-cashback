using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Repository.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Tests.Fakes
{
    public class FakeOrderRepository : IOrderRepository
    {
        public List<Order> orders = new List<Order>();

        public void InsertFake(Order order)
        {
            order.Id = Guid.NewGuid().ToString("N");
            orders.Add(order);
        }

        public async Task DeleteAsync(Order order)
        {
            orders.Remove(order);
            await Task.CompletedTask;
        }

        public async Task<ICollection<Order>> GetItemsAsync()
        {
            await Task.CompletedTask;
            return orders;
        }

        public async Task<Order> GetByCode(string code)
        {
            await Task.CompletedTask;
            return orders.FirstOrDefault(c => c.Code == code);
        }

        public async Task<Order> GetById(string id)
        {
            await Task.CompletedTask;
            return orders.FirstOrDefault(c => c.Id == id);
        }

        public Task<Order> InsertAsync(Order order)
        {
            order.Id = Guid.NewGuid().ToString("N");
            orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> UpdateAsync(Order order)
        {
            var item = orders.FirstOrDefault(c => c.Code == order.Code);
            if (item == null)
                orders.Add(order);
            else
            {
                item.PrepareToUpdate(order.Code, order.Value, order.Date, order.Cpf);
            }

            return Task.FromResult(item);
        }
    }
}
