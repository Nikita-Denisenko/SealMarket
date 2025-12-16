using SealMarket.Application.DTOs.Requests.AuthDTOs;
using SealMarket.Application.DTOs.Responses.AuthDTOs;

namespace SealMarket.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResultDto> Register(RegisterDto registerDto);

        public Task<AuthResultDto> Login(LoginDto loginDto);
    }
}
