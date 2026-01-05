using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.TransactionDtos;
using SealMarket.Application.Helpers;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreatedTransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto)
        {
            var transaction = new Transaction
            (
                createTransactionDto.AccountId,
                createTransactionDto.TotalSum,
                createTransactionDto.IsSuccessful,
                createTransactionDto.Message
            );

            await _repo.AddAsync(transaction);
            await _repo.SaveChangesAsync();

            return new CreatedTransactionDto
            (
                transaction.Id,
                transaction.AccountId
            );
        }

        public async Task<TransactionDto> GetTransactionAsync(int id)
        {
            var transaction = await _repo.GetTransactionWithIncludesAsync(id);

            if (transaction is null)
                throw new KeyNotFoundException($"Transaction with id {id} not found.");

            return new TransactionDto
            (
                id,
                transaction.AccountId,
                transaction.Account!.Login,
                transaction.Account.Email,
                transaction.Account.Balance,
                transaction.TotalSum,
                transaction.IsSuccessful,
                transaction.Message,
                transaction.CreatedAt
            );
        }

        public async Task<List<ShortTransactionDto>> GetTransactionsAsync(TransactionsFilterDto filterDto, int? accountId)
        {
            var filter = new TransactionsFilter
            (
                filterDto.Page,
                filterDto.Size,
                filterDto.FromDateTime ?? DateTimeHelper.MinDateToFilter,
                filterDto.ToDateTime ?? DateTimeHelper.MaxDateToFilter,
                filterDto.OrderParam,
                filterDto.ByAscending
            );

            var transactions = await _repo.GetTransactionsAsync(filter, accountId);

            return transactions.Select(transaction => new ShortTransactionDto
            (
                transaction.Id,
                transaction.AccountId,
                transaction.TotalSum,
                transaction.IsSuccessful,
                transaction.Message,
                transaction.CreatedAt
            )).ToList();
        }
    }
}
