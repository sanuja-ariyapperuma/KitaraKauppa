using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Core.Products
{
    public class Image : BaseEntity
    {
        public string ImageAlt { get; set; } = String.Empty;
        public string Extention { get; set; } = String.Empty;
        public ICollection<Product> Product { get; set; } = null!;
    }
}
