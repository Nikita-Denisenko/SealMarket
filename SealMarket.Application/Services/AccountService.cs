using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using System.Linq;
using System.Net.WebSockets;

namespace SealMarket.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreatedAccountDto> CreateAccountAsync(int userId, CreateAccountDto createAccountDto)
        {
            if (createAccountDto is null)
                throw new ArgumentNullException(nameof(createAccountDto));

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

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account is null)
                throw new KeyNotFoundException("Account to delete was not found.");

            _repo.Delete(account);

            await _repo.SaveChangesAsync();
        }

        public async Task<ReadAccountDto> GetAccountByIdAsync(int id)
        {
            var account = await _repo.GetAccountWithIncludesAsync(id);

            if (account is null)
                throw new KeyNotFoundException("Account was not found.");

            return new ReadAccountDto
            (
                account.Id,
                account.UserId,
                account.Balance,
                account.Login,
                account.Email,
                account.PhoneNumber,
                account.CreatedAt,
                account.Cart.Id,
                account.Notifications
                    .Select(n => new ReadNotificationDto
                     (
                          n.Id,
                          n.Message,
                          n.DateTime,
                          n.HasBeenRead,
                          n.AccountId
                           )
                     ).ToList()
            );
        }

        public async Task<List<ReadAccountDto>> GetAccountsAsync(AccountsFilterDto accountsFilterDto)
        {
            if (accountsFilterDto is null)
                throw new ArgumentNullException(nameof(accountsFilterDto));

            var filter = new AccountsFilter
            (
                accountsFilterDto.Page,
                accountsFilterDto.Size,
                accountsFilterDto.MinBalance,
                accountsFilterDto.MaxBalance,
                accountsFilterDto.OrderParam,
                accountsFilterDto.ByAscending,
                accountsFilterDto.SearchText
            );
            
            var accounts = await _repo.GetAccountsAsync(filter);

            var readAccountDtos = accounts
                .Select(account => new ReadAccountDto
                    (
                        account.Id,
                        account.UserId,
                        account.Balance,
                        account.Login,
                        account.Email,
                        account.PhoneNumber,
                        account.CreatedAt,
                        account.Cart.Id,
                        account.Notifications
                            .Select(n => new ReadNotificationDto
                                (
                                    n.Id,
                                    n.Message,
                                    n.DateTime,
                                    n.HasBeenRead,
                                    n.AccountId
                                )
                            ).ToList()
                    )
                ).ToList();

            return readAccountDtos;
        }

        public async Task UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto)
        {
            if (updateAccountDto is null)
                throw new ArgumentNullException(nameof(updateAccountDto));

            var account = await _repo.GetByIdAsync(id);

            if (account is null)
                throw new KeyNotFoundException("Account to update was not found.");

            account.UpdateAcccountData
            (
                updateAccountDto.Login ?? account.Login,
                updateAccountDto.Password ?? account.Password,
                updateAccountDto.Email ?? account.Email,
                updateAccountDto.PhoneNumber ?? account.PhoneNumber
            );

            await _repo.SaveChangesAsync();
        }
    }
}