using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public Task<List<Account>> GetAccountsAsync(AccountsFilter filter);
    }
}
