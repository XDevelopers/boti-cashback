using Boticario.EuRevendedor.Interfaces.Service;
using Boticario.EuRevendedor.Interfaces.Models;
using Boticario.EuRevendedor.Interfaces.Repository;
using System.Collections.Generic;

namespace Boticario.EuRevendedor.Service
{
    public class OrderBusiness : IOrderBusiness
    {
        private IOrderRepository orderRepository;

        public OrderBusiness(IOrderRepository repository)
        {
            orderRepository = repository;
        }

        public IOrder AddOrderAsync(IOrder order)
        {
            return orderRepository.Add(order);
        }

        public IEnumerable<IOrder> GetAll()
        {
            return orderRepository.FindAll();
        }
    }
}
