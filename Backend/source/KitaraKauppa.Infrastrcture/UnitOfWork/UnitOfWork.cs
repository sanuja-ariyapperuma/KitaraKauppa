using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace KitaraKauppa.Infrastrcture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KitaraKauppaDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
