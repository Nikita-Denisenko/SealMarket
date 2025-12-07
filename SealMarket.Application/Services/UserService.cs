using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) 
        {
            _repo = repo;
        }

        public Task<CreatedUserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReadUserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadUserDto> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReadUserDto>> GetUsersAsync(UsersFilterDto usersfilterDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
