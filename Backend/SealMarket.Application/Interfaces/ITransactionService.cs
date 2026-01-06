using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.TransactionDtos;

namespace SealMarket.Application.Interfaces
{
    public interface ITransactionService
    {
        public Task<CreatedTransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto);
        public Task<TransactionDto> GetTransactionAsync(int id);
        public Task<List<ShortTransactionDto>> GetTransactionsAsync(TransactionsFilterDto filterDto, int? accountId);
    }
}
