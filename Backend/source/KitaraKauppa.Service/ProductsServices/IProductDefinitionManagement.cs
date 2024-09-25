using KitaraKauppa.Service.ProductsServices.Dtos.Definition;
using KitaraKauppa.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices
{
    public interface IProductDefinitionManagement
    {
        public Task<KKResult<ProductDefinitionDto>> GetDefinitions();
    }
}
