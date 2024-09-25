using KitaraKauppa.Core.Users;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.Repositories.Cities;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Cities
{
    public class CityRepository : ICityRepository
    {
        private readonly KitaraKauppaDbContext _context;
        public CityRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public async Task<City?> GetCity(Guid cityId)
        {
            return await _context.Cities.FindAsync(cityId);
        }

        public async Task<bool> IsCityExists(Guid cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }
    }
}
