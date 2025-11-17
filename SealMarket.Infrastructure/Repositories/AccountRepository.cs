using Microsoft.EntityFrameworkCore;
using static SealMarket.Core.Constans.AccountOrderParameters;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;

namespace SealMarket.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(AppDbContext context) : base(context) { }

        public async Task<List<Account>> GetAccountsAsync(AccountsFilter filter)
        {
            var query = _context.Accounts.AsQueryable();

            query = query
                .Where(account => account.Balance >= filter.MinBalance && account.Balance <= filter.MaxBalance);


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
    }
}
