using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos.Definition
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
