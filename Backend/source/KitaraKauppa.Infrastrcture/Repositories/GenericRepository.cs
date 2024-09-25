using KitaraKauppa.Core.Shared;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.Shared;
using KitaraKauppa.Service.Shared_Dtos;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly KitaraKauppaDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(KitaraKauppaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var savedentity = await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return savedentity.Entity;
        }
        public async Task<bool> DeleteAsync(T entity)
        {

            entity.IsDeleted = true;
            _dbContext.Entry(entity).Property(x => x.IsDeleted).IsModified = true;
            await _dbContext.SaveChangesAsync();
            return true;
            
        }
        public async Task<T?> GetByIdAsync(Guid id)=> await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}
