using KitaraKauppa.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices.Dtos
{
    public class UpdateProductDto : ReadProductDto
    {
        public override KKResult<string> Validate()
        {
            if(Id == Guid.Empty)
                return new KKResult<string>().Fail("Id is required");
            
            return base.Validate();
        }
    }
    
}
