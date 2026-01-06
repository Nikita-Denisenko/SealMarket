using SealMarket.Application.DTOs.Requests.AuthDTOs;
using SealMarket.Application.DTOs.Responses.AuthDTOs;

namespace SealMarket.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResultDto> RegisterAsync(RegisterDto registerDto);

        public Task<AuthResultDto> LoginAsync(LoginDto loginDto);
    }
}
