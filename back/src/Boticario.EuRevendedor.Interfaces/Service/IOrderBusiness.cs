using Boticario.EuRevendedor.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Interfaces.Service
{
    public interface IOrderBusiness
    {
        IOrder AddOrderAsync(IOrder order);

        IEnumerable<IOrder> GetAll();
    }
}
