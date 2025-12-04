namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<int> SaveChangesAsync();

        public Task<List<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(int id);

        public Task AddAsync(T entity);

        public Task AddRangeAsync(IEnumerable<T> entities);

        public void Update(T entity);

        public void UpdateRange(IEnumerable<T> entities);

        public void Delete(T entity);

        public void DeleteRange(IEnumerable<T> entities);

        public Task DeleteByIdAsync(int id);
        public Task<bool> ExistsAsync(int id);

        public Task ClearAllAsync();
    }
}
