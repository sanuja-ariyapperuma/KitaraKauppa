using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Core.Users;
using KitaraKauppa.Infrastrcture.Database;
using Microsoft.EntityFrameworkCore;
using KitaraKauppa.Service.UsersService;
using System.Linq;
using KitaraKauppa.Service.Shared_Dtos;
using KitaraKauppa.Service.UsersService.Dtos;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.Repositories.Products;

namespace KitaraKauppa.Infrastrcture.Repositories.Users
{
    public class UserRepository :   GenericRepository<User>, IUserRepository
    {

        public UserRepository(KitaraKauppaDbContext context) : base(context) {}

        public override Task<User?> GetByIdAsync(Guid userId) => 
            _dbSet
                .Include(u => u.UserRole)
                .Include(u => u.UserContactNumbers)
                .Include(u => u.UserAddresses)
                .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(u => u.Id == userId);
        

        public async Task<List<User>> GetUsers(UsersQueryOptions userquery)
        {

            var query = _dbSet
                .Include(u => u.UserRole)
                .AsNoTracking()
                .AsQueryable();

            if (userquery.UserRoleId.HasValue)
            {
                query.Where(u => u.UserRoleId == userquery.UserRoleId);
            }

            if (!string.IsNullOrEmpty(userquery.Search))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(userquery.Search) ||
                    u.LastName.Contains(userquery.Search) ||
                    u.Email.Contains(userquery.Search));
            }

            var isAscending = userquery.OrderBy == OrderBy.ASC;

            query = userquery.OrderUserWith switch
            {
                UsersOrderWith.FirstName => isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                UsersOrderWith.LastName => isAscending ? query.OrderBy(u => u.UserRole.UserRoleName) : query.OrderByDescending(u => u.UserRole.UserRoleName),
                _ => isAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName)
            };

            var users = await query
                .Skip((userquery.PageNo - 1) * userquery.PageSize)
                .Take(userquery.PageSize)
                .ToListAsync();

            return users;
        }

        public Task<bool> CheckUserExistsByEmail(string email) =>
            _dbSet.AsNoTracking().Where(s => s.Email == email).AnyAsync();
        

        public Task<User?> GetUserByUsername(string username) =>
            _dbSet
                .AsNoTracking()
                .Include(u => u.UserRole)
                .Include(u => u.UserCredential)
                .FirstOrDefaultAsync(u => (u.UserCredential.UserName == username) && (u.IsUserActive ?? false));
        

        public Task<bool> CheckEmailExistsWithOtherUsers(Guid userId, string email) => 
            _dbSet.AsNoTracking().Where(s => s.Email == email && s.Id != userId).AnyAsync();
        
    }
}
