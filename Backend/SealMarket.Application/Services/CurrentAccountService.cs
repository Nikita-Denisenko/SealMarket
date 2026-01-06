using SealMarket.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SealMarket.Application.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAccountService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? AccountId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User
                    .FindFirstValue("account_id");
                return int.TryParse(claim, out var id) ? id : null;
            }
        }

        public int? UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User
                    .FindFirstValue("user_id");
                return int.TryParse(claim, out var id) ? id : null;
            }
        }

        public string? Email => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Email);

        public string? Role => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Role);

        public bool IsAuthenticated => AccountId.HasValue;
    }
}
