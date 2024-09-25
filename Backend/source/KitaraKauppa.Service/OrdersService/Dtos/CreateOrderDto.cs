using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.OrdersService.Dtos
{
    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public List<CreateOrderItems> OrderItems { get; set; } = [];
    }
}
