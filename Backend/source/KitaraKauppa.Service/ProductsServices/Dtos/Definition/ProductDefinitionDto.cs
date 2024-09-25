using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos.Definition
{
    public class ProductDefinitionDto
    {

        public IEnumerable<BrandDto> Brands { get; set; } = [];
        public IEnumerable<ColorDto> Colors { get; set; } = [];
        public IEnumerable<string> Oriantations { get; set; } = [];
        public IEnumerable<string> Variants { get; set; } = [];

    }
}
