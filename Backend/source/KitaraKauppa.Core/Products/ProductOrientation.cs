using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Core.Products
{
    public class ProductOrientation : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Orientation Orientation { get; set; }
        public ICollection<Product> Product { get; set; } = null!;
    }
}
