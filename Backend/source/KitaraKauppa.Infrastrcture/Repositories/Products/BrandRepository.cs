using KitaraKauppa.Core.Products;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.Repositories.Brand;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Products
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(KitaraKauppaDbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Brand>> GetBrandAsync() => await  _dbSet.ToListAsync();
        
    }
}
