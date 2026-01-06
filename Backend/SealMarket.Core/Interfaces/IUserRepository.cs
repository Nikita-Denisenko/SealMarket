using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<List<User>> GetUsersAsync(UsersFilter filter);
        public Task<User?> GetWithAccountAsync(int id);
    }
}

