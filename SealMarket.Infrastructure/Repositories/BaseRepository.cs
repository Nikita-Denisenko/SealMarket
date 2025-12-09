using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces;
using SealMarket.Infrastructure.Data;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected DbSet<T> Set => _context.Set<T>();

    public BaseRepository(AppDbContext context) => _context = context;

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await Set.FindAsync(id);

    public virtual async Task AddAsync(T entity)
        => await Set.AddAsync(entity);

    public virtual void Update(T entity)
        => Set.Update(entity);

    public virtual void Delete(T entity)
        => Set.Remove(entity);

    public virtual async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            Delete(entity);
    }

    public virtual async Task<bool> ExistsAsync(int id)
        => await Set.AnyAsync(e => EF.Property<int>(e, "Id") == id);
}
