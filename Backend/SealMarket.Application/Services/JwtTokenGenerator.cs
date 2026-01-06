using Microsoft.IdentityModel.Tokens;
using SealMarket.Application.Interfaces;
using SealMarket.Application.Interfaces.SealMarket.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SealMarket.Application.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenGenerator(string jwtKey, string issuer, string audience)
        {
            _jwtKey = jwtKey;
            _issuer = issuer;
            _audience = audience;
        }

        public JwtTokenGenerator()
            : this(
                "SuperSecretKey1234567890abcdefghijklmnopqrstuvwxyz",
                "SealMarket",
                "SealMarketUsers")
        {
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}