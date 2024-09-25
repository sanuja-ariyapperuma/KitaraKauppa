using KitaraKauppa.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Brand
{
    public interface IBrandRepository
    {
        public Task<ICollection<Core.Products.Brand>> GetBrandAsync();
    }
}
