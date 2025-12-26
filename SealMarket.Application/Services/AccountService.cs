using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.AccountDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AccountDashboardDto> GetAccountAsync(int id)
        {
            var account = await _repo.GetAccountWithIncludesAsync(id);

            if (account is null)
                throw new KeyNotFoundException("Account was not found.");

            return new AccountDashboardDto
            (
                account.Id,
                account.Balance,
                account.CreatedAt,
                account.Cart.Id,
                account.Cart.CartItems.Count,
                account.Notifications.Where(n => !n.HasBeenRead).Count()
            );
        }

        public async Task<List<ShortAccountDto>> GetAccountsAsync(AccountsFilterDto accountsFilterDto)
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

            var shortAccountDtos = accounts
                .Select(account => new ShortAccountDto
                    (
                        account.Id,
                        account.Login,
                        account.UserId
                    )).ToList();

            return shortAccountDtos;
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