namespace SealMarket.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<int> SaveChangesAsync();

        public Task<T?> GetByIdAsync(int id);

        public Task AddAsync(T entity);

        public void Delete(T entity);

        public Task DeleteByIdAsync(int id);

        public Task<bool> ExistsAsync(int id);
    }
}
