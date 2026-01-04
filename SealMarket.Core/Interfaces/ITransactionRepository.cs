using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;


namespace SealMarket.Core.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetTransactionsAsync(TransactionsFilter filter, int? accountId);
        public Task<Transaction?> GetTransactionWithIncludes(int id);
    }
}
