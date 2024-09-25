using KitaraKauppa.Core.Shared;
using KitaraKauppa.Service.Shared_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class ProductQueryOptions : QueryOptions<ProductOrderOptions>
    {
        public VarientType? VarientType { get; set; }
        public ProductQueryOptions()
        {
            OrderWith = ProductOrderOptions.ProductTitle;
        }

    }
}
