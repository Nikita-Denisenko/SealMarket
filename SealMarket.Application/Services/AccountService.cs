using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }
    }
}
    