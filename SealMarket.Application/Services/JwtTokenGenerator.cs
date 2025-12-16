namespace SealMarket.Application.Services
{
    using global::SealMarket.Application.Interfaces.SealMarket.Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    namespace SealMarket.Application.Services
    {
        public class JwtTokenGenerator : IJwtTokenGenerator
        {
            private readonly IConfiguration _configuration;

            public JwtTokenGenerator(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateToken(int accountId, int userId, string email, string fullName)
            {
                return GenerateToken(accountId, userId, email, fullName, "Customer");
            }

            public string GenerateToken(int accountId, int userId, string email,
                string fullName, string role)
            {
                var claims = new[]
                {
                new Claim("account_id", accountId.ToString()),
                new Claim("user_id", userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("full_name", fullName),
                new Claim(ClaimTypes.Role, role)
            };

                return GenerateToken(claims);
            }

            public string GenerateToken(IEnumerable<Claim> claims)
            {
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}