using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public AccountService(IBaseRepository<Account> repo) : base(repo) { }
    }
}
    