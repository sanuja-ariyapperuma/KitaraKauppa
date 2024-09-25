using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Orders;

namespace KitaraKauppa.Service.Repositories.Orders
{
    public interface IOrderItemsRepository
    {
        public Task<bool> CreateOrderItems(OrderItem cartItem);
        public Task<OrderItem> GetOrderItem(Guid cartItemId);
    }
}