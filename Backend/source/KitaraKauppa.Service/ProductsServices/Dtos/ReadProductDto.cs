using KitaraKauppa.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class ReadProductDto : CreateProductDto
    {
        public Guid Id { get; set; }
        public List<ImageReadDto> Images { get; set; }
    }
}
