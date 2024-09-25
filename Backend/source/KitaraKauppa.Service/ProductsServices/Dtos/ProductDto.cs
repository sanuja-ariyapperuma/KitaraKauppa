using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public string ImageAlt { get; set; } = String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
        public double Price { get; set; }
    }
}
