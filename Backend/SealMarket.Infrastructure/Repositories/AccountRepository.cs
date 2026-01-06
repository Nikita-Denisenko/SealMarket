using Microsoft.EntityFrameworkCore;
using static SealMarket.Core.Constants.AccountOrderParameters;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using SealMarket.Core.Interfaces;
using System.Dynamic;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace SealMarket.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context) { }

        public async Task<List<Account>> GetAccountsAsync(AccountsFilter filter)
        {
            var query = _context.Accounts
                .AsQueryable();

            query = query
               .Where(a => a.Login.Contains(filter.SearchText));

            query = query
                .Where(a => a.Balance >= filter.MinBalance && a.Balance <= filter.MaxBalance);

            query = filter.OrderParam switch
            {
                Balance => filter.ByAscending
                    ? query.OrderBy(a => a.Balance)
                    : query.OrderByDescending(a => a.Balance),
                
               DateCreated => filter.ByAscending
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

        public async Task<Account?> GetAccountWithIncludesAsync(int id)
        {
            var account = await _context.Accounts
                 .Include(a => a.User)
                 .Include(a => a.Notifications)
                 .Include(a => a.Cart)
                 .FirstOrDefaultAsync(a => a.Id == id);

            return account;
        }

        public async Task<bool> IsLoginTakenAsync(string login)
            => await _context.Accounts.AnyAsync(account => account.Login == login);
        
        public async Task<bool> IsEmailTakenAsync(string email) 
            => await _context.Accounts.AnyAsync(account => account.Email == email);

        public async Task<Account?> GetByLoginAsync(string login)
             => await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Login == login);
    }
}
