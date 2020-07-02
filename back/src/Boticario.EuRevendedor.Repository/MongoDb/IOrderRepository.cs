using Boticario.EuRevendedor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public interface IOrderRepository
    {
        Task<Order> GetById(string id);

        Task<Order> GetByCode(string code);

        Task<ICollection<Order>> GetItemsAsync();

        Task<Order> InsertAsync(Order order);

        Task<Order> UpdateAsync(Order order);

        Task DeleteAsync(Order order);
    }
}
