using KitaraKauppa.Core.Users;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Users
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly KitaraKauppaDbContext _context;

        public UserRoleRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public async Task<UserRole?> GetUserRoleByName(string roleName)=> await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(ur => ur.UserRoleName == roleName);
        
    }
}
