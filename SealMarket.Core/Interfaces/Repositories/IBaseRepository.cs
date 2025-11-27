namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T: class
    {
        public Task SaveChangesAsync();

        public Task<List<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(int id);

        public Task AddAsync(T entity);

        public Task DeleteByIdAsync(int id);

        public Task UpdateAsync(T entity);

        public Task ClearAllAsync();
    }
}
