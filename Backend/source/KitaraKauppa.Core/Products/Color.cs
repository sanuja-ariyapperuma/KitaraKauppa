using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Core.Products
{
    public class Color : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
