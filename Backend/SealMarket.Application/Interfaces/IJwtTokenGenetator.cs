namespace SealMarket.Application.Interfaces
{
    using System.Security.Claims;

    namespace SealMarket.Application.Interfaces
    {
        public interface IJwtTokenGenerator
        {
            string GenerateToken(int accountId, int userId, string email, string fullName);

            string GenerateToken(int accountId, int userId, string email,
                string fullName, string role);

            string GenerateToken(IEnumerable<Claim> claims);
        }
    }
}
