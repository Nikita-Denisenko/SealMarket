using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs;
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

        public async Task<CreatedUserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto is null)
                throw new ArgumentNullException(nameof(createUserDto));

            var user = new User
            (
                createUserDto.Name,
                createUserDto.BirthDate,
                createUserDto.City
            );

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            var createdUser = new CreatedUserDto(user.Id, user.Name);

            return createdUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException("User to delete was not found");

            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<ReadUserDto> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetWithAccountAsync(id);

            if (user is null)
                throw new KeyNotFoundException("User was not found.");

            return new ReadUserDto
            (
                user.Id,
                user.Name,
                user.BirthDate,
                user.City,
                user.Account?.Id
            );
        }

        public async Task<List<ReadUserDto>> GetUsersAsync(UsersFilterDto usersfilterDto)
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
                .Select(user => new ReadUserDto
                    (
                        user.Id,
                        user.Name,
                        user.BirthDate,
                        user.City,
                        user.Account?.Id
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
