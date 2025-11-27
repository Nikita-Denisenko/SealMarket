using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repo;

        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _repo.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<T>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task ClearAllAsync()
            => await _repo.ClearAllAsync();
    }
}
    
