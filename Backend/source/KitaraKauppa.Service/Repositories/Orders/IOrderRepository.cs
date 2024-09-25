using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.Shared;

namespace KitaraKauppa.Service.Repositories.Orders
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrder(Order order);
        public Task <Order?> GetOrder(Guid orderId);
        public Task<bool> UpdateOrder(Guid orderId, OrderStatus orderStatus);
        public Task<KKResult<IEnumerable<Order>>> GetAllOrders(); 
    }
}