using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.OrdersService.Dtos
{
    public class CreateOrderItems
    {
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public Orientation Orientation { get; set; }
        public int Units { get; set; }

        //Protecting value getting updated from the API
        public decimal Price { get; private set; }
        public void SetPrice(decimal price) => Price = price;
        
    }
}
