using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<List<User>> GetUsersAsync(UsersFilter filter);
    }
}

