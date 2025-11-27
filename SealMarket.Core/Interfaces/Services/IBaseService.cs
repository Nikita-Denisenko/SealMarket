namespace SealMarket.Core.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        public Task<T?> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task DeleteByIdAsync(int id);
        public Task ClearAllAsync();
    }
}
