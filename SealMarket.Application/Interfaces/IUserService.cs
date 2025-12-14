using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.UserDtos;

namespace SealMarket.Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<ShortUserDto>> GetUsersAsync(UsersFilterDto usersfilterDto);
        public Task<UserProfileDto> GetUserProfileAsync(int id);
        public Task<PublicUserDto> GetPublicUserProfileAsync(int id);
        public Task<CreatedUserDto> CreateUserAsync(CreateUserDto createUserDto);
        public Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        public Task DeleteUserAsync(int id);
    }
}
