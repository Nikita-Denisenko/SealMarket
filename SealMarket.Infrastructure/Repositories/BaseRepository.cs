using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Infrastructure.Data;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected DbSet<T> Set => _context.Set<T>();

    public BaseRepository(AppDbContext context) => _context = context;

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public virtual async Task<List<T>> GetAllAsync()
        => await Set.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await Set.FindAsync(id);

    public virtual async Task AddAsync(T entity)
        => await Set.AddAsync(entity);

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        => await Set.AddRangeAsync(entities);

    public virtual void Update(T entity)
        => Set.Update(entity);

    public virtual void UpdateRange(IEnumerable<T> entities)
        => Set.UpdateRange(entities);

    public virtual void Delete(T entity)
        => Set.Remove(entity);

    public virtual void DeleteRange(IEnumerable<T> entities)
        => Set.RemoveRange(entities);

    public virtual async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            Delete(entity);
    }

    public virtual async Task<bool> ExistsAsync(int id)
        => await Set.AnyAsync(e => EF.Property<int>(e, "Id") == id);

    public virtual async Task ClearAllAsync()
    {
        var all = await Set.ToListAsync();
        Set.RemoveRange(all);
    }
}
