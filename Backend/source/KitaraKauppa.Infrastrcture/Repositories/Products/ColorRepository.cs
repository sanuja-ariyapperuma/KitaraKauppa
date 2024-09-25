using KitaraKauppa.Core.Products;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.Shared;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Products
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(KitaraKauppaDbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Color>> GetColorsAsync() => await _dbSet.ToListAsync();

        public async Task<ICollection<Color>> GetColorsByIdsAsync(Guid[] ids) =>
            await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
    }
}
