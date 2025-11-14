using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        public Task SaveChangesAsync();
        public Task<Account> GetByIdAsync(int id);
        public Task<List<Account>> GetAllAsync();
        public Task AddAsync(Account Account);
        public Task DeleteByIdAsync(int id);
        public Task<List<Account>> GetAccounts(AccountsFilter accountsFilter);
        public Task UpdateAccount(Account account);
    }
}
