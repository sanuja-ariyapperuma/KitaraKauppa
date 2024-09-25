using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class ImageReadDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = String.Empty;
        public string ImageAlt { get; set; } = String.Empty;
    }
}
