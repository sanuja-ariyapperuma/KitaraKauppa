using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Core.Users;
using KitaraKauppa.Infrastrcture.Database;
using Microsoft.EntityFrameworkCore;
using KitaraKauppa.Service.UsersService;
using System.Linq;
using KitaraKauppa.Service.Shared_Dtos;
using KitaraKauppa.Service.UsersService.Dtos;

namespace KitaraKauppa.Infrastrcture.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly KitaraKauppaDbContext _context;

        public UserRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            user.LastLogin = DateTime.UtcNow;
            var savedUser = (await _context.Users.AddAsync(user)).Entity;
            _context.SaveChanges();

            savedUser = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == savedUser.Id);

            return savedUser;
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUser(Guid userId)
        {
            return _context.Users
                .Include(u => u.UserRole)
                .Include(u => u.UserContactNumbers)
                .Include(u => u.UserAddresses)
                .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetUsers(UsersQueryOptions userquery)
        {

            var query = _context.Users
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

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> CheckUserExistsByEmail(string email)
        {
            return _context.Users.AsNoTracking().Where(s => s.Email == email).AnyAsync();
        }

        public Task<User?> GetUserByUsername(string username)
        {
            return _context.Users
                .AsNoTracking()
                .Include(u => u.UserRole)
                .Include(u => u.UserCredential)
                .FirstOrDefaultAsync(u => (u.UserCredential.UserName == username) && (u.IsUserActive ?? false));
        }

        public Task<bool> CheckEmailExistsWithOtherUsers(Guid userId, string email)
        {
            return _context.Users.AsNoTracking().Where(s => s.Email == email && s.Id != userId).AnyAsync();
        }
    }
}
