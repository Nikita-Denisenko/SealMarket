using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;

namespace SealMarket.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<List<ReadAccountDto>> GetAccountsAsync(AccountsFilterDto accountsFilterDto);
        public Task<ReadAccountDto> GetAccountByIdAsync(int id);
        public Task<CreatedAccountDto> CreateAccountAsync(int userId, CreateAccountDto createAccountDto);
        public Task UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto);
        public Task DeleteAccountAsync(int id);
    }
}
