using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Constans;
using SealMarket.Core.Entities;
using SealMarket.Core.Filters;
using SealMarket.Infrastructure.Data;

namespace SealMarket.Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
            => await _context.Accounts.FindAsync(id);
        
        public async Task<List<Account>> GetAllAsync()
            => await _context.Accounts.ToListAsync();

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var accountToDelete = await GetByIdAsync(id);

            if (accountToDelete is null)
                throw new Exception("Account is not found.");

            _context.Accounts.Remove(accountToDelete);
            await SaveChangesAsync();
        }

        public async Task<List<Account>> GetAccounts(AccountsFilter filter)
        {
            var query = _context.Accounts.AsQueryable();

            query = query
                .Where(account => account.Balance >= filter.MinBalance && account.Balance <= filter.MaxBalance);


            query = filter.OrderParam switch
            {
                AccountOrderParameters.Balance => filter.ByAscending
                                                    ? query.OrderBy(a => a.Balance)
                                                    : query.OrderByDescending(a => a.Balance),
                
                AccountOrderParameters.DateCreated => filter.ByAscending
                                                   ? query.OrderBy(a => a.CreatedAt)
                                                   : query.OrderByDescending(a => a.CreatedAt),
                
                _ => filter.ByAscending
                    ? query.OrderBy(a => a.Login)
                    : query.OrderByDescending(a => a.Login)
            };

            query = query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size);

            return await query.ToListAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            await SaveChangesAsync();
        }
    }
}
