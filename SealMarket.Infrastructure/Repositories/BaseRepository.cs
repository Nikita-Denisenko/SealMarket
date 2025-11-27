using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Infrastructure.Data;

namespace SealMarket.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) => _context = context;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public virtual async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);

            if (entityToDelete is null)
                throw new Exception($"{typeof(T).Name} with id {id} not found");

            _context.Set<T>().Remove(entityToDelete);

            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task ClearAllAsync()
        {
            var entitiesToDelete = await GetAllAsync();
            _context.Set<T>().RemoveRange(entitiesToDelete);
            await SaveChangesAsync();
        }
    }
}
