using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.AccountDtos;

namespace SealMarket.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<List<ShortAccountDto>> GetAccountsAsync(AccountsFilterDto accountsFilterDto);
        public Task<AccountDashboardDto> GetAccountAsync(int id);
        public Task UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto);
    }
}
