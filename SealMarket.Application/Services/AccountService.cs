using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public Task<CreatedAccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReadAccountDto> GetAccountByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReadAccountDto>> GetAccountsAsync(AccountsFilterDto accountsFilterDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto)
        {
            throw new NotImplementedException();
        }
    }
}
    