using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Products
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        public Task<ICollection<Color>> GetColorsByIdsAsync(Guid[] ids);
        public Task<ICollection<Color>> GetColorsAsync();
    }
}
