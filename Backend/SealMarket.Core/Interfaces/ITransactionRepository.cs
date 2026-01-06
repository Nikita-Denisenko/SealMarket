using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;


namespace SealMarket.Core.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public Task<List<Transaction>> GetTransactionsAsync(TransactionsFilter filter, int? accountId);
        public Task<Transaction?> GetTransactionWithIncludesAsync(int id);
    }
}
