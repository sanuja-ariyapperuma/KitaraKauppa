using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.OrdersService.Dtos
{
    public class OrderItemDto
    {
        public string Title { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public int Quantity { get; set; }
        public Orientation Orientation { get; set; }
    }
}
