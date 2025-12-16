using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.UserDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) 
        {
            _repo = repo;
        }

        public async Task DeleteUserAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException("User to delete was not found");

            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<PublicUserDto> GetPublicUserProfileAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user is null)
                throw new KeyNotFoundException("User was not found.");

            return new PublicUserDto
            (
                id,
                user.Name,
                user.BirthDate,
                user.City
            );
        }

        public async Task<UserProfileDto> GetUserProfileAsync(int id)
        {
            var user = await _repo.GetWithAccountAsync(id);

            if (user is null)
                throw new KeyNotFoundException("User was not found.");

            if (user.Account is null)
                throw new InvalidOperationException($"User {id} has no account");

            return new UserProfileDto
            (
                user.Id,
                user.Name,
                user.BirthDate,
                user.City,
                user.Account.Id,
                user.Account.Login,
                user.Account.Email,
                user.Account.PhoneNumber,
                user.Account.Balance,
                user.Account.CreatedAt
            );
        }

        public async Task<List<ShortUserDto>> GetUsersAsync(UsersFilterDto usersfilterDto)
        {
            if (usersfilterDto is null)
                throw new ArgumentNullException(nameof(usersfilterDto));

            var filter = new UsersFilter
            (
                usersfilterDto.Page,
                usersfilterDto.Size,
                usersfilterDto.MinAge,
                usersfilterDto.MaxAge,
                usersfilterDto.OrderParam,
                usersfilterDto.ByAscending,
                usersfilterDto.SearchText
            );

            var users = await _repo.GetUsersAsync(filter);

            var readUserDtos = users
                .Select(user => new ShortUserDto
                    (
                        user.Id,
                        user.Name
                    )
                ).ToList();

            return readUserDtos;
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            if (updateUserDto is null)
                throw new ArgumentNullException(nameof(updateUserDto));

            var user = await _repo.GetByIdAsync(id);

            if (user is null)
                throw new KeyNotFoundException("User was not found.");

            user.UpdatePersonalInfo
            (
                updateUserDto.Name ?? user.Name, 
                updateUserDto.City ?? user.City
            );

            await _repo.SaveChangesAsync();
        }
    }
}
