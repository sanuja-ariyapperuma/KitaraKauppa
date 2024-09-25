using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.OrdersService.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string ShippingAddress { get; set; } = String.Empty;
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
