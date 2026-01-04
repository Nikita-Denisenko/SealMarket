using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constants.TransactionOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context){}

        public async Task<List<Transaction>> GetTransactionsAsync(TransactionsFilter filter, int? accountId)
        {
            var query = _context.Transactions.AsQueryable();

            if (accountId.HasValue)
                query = query.Where(t => t.AccountId == accountId.Value);

            query = query
                .Where(t => t.CreatedAt >= filter.FromDateTime && t.CreatedAt <= filter.ToDateTime);

            query = filter.OrderParam switch
            {
                TotalSum => filter.ByAscending
                    ? query.OrderBy(t => t.TotalSum)
                    : query.OrderByDescending(t => t.TotalSum),

                _ => filter.ByAscending
                    ? query.OrderBy(t => t.CreatedAt)
                    : query.OrderByDescending(t => t.CreatedAt),
            };

            return await query
                .Skip((filter.Page - 1) * filter.Size)
                .Take(filter.Size)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionWithIncludes(int id)
            => await _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id);
    }
}