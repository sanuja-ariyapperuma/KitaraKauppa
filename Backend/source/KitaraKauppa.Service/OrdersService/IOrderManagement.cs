using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.OrdersService.Dtos;
using KitaraKauppa.Service.Shared;

namespace KitaraKauppa.Service.OrdersService
{
    public interface IOrderManagement
    {
        public Task<KKResult<ReadOrderDto>> CreateOrder(CreateOrderDto newOrder);
        public Task<KKResult<ReadOrderDto>> GetOrder(Guid orderId);
        public Task <bool> UpdateOrder(Guid orderId, OrderStatus orderStatus);
        public Task<KKResult<IEnumerable<OrderDto>>> GetAllOrders();
        
    }
}