using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using System.Net.WebSockets;

namespace SealMarket.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IUserRepository _userRepo;

        public AccountService(IAccountRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        public async Task<CreatedAccountDto> CreateAccountAsync(int userId, CreateAccountDto createAccountDto)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user is null)
                throw new KeyNotFoundException("User to create account was not found");

            if (createAccountDto is null)
                throw new ArgumentNullException(nameof(createAccountDto));

            if (user.Account != null)
                throw new InvalidOperationException("User already has account.");

            var account = new Account
            (
                userId,
                createAccountDto.Login,
                createAccountDto.Password,
                createAccountDto.Email,
                createAccountDto.PhoneNumber
            );

            await _repo.AddAsync(account);
            await _repo.SaveChangesAsync();

            return new CreatedAccountDto
            (
                account.Id,
                account.UserId,
                account.Login,
                account.CreatedAt
            );
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