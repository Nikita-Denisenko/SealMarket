using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public Task<List<Account>> GetAccountsAsync(AccountsFilter filter);
        public Task<Account?> GetAccountWithIncludesAsync(int id);
        public Task<bool> IsLoginTakenAsync(string login);
        public Task<bool> IsEmailTakenAsync(string email);
        public Task<Account?> GetByLoginAsync(string login);
    }
}
