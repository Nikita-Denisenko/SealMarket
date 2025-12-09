using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs;

namespace SealMarket.Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<ReadUserDto>> GetUsersAsync(UsersFilterDto usersfilterDto);
        public Task<ReadUserDto> GetUserAsync(int id);
        public Task<CreatedUserDto> CreateUserAsync(CreateUserDto createUserDto);
        public Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        public Task DeleteUserAsync(int id);
    }
}
