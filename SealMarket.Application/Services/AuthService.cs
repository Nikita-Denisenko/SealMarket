using SealMarket.Application.DTOs.Requests.AuthDTOs;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Responses.AuthDTOs;
using SealMarket.Application.Interfaces;
using SealMarket.Application.Interfaces.SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using SealMarket.Application.Constants;

namespace SealMarket.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService
        (
            IUserRepository userRepo,
            IAccountRepository accountRepo, 
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator tokenGenerator
        )
        {
            _userRepo = userRepo;
            _accountRepo = accountRepo;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResultDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResultDto> Register(RegisterDto registerDto)
        {
            if (registerDto is null) 
                throw new ArgumentNullException(nameof(registerDto));

            var login = registerDto.Login;

            if (await _accountRepo.IsLoginTakenAsync(login))
                throw new Exception("Login is already exists");

            var email = registerDto.Email;

            if (await _accountRepo.IsEmailTakenAsync(email))
                throw new Exception("Email is already exists");

            var role = Roles.Customer;

            if(!string.IsNullOrEmpty(registerDto.SecretCode) 
                && registerDto.SecretCode == AuthConstants.AdminSecretCode)
            {
                role = Roles.Admin;
            }

            var user = new User
            (
                registerDto.UserName, 
                registerDto.BirthDate, 
                registerDto.City
            );

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            var passwordHash = _passwordHasher.HashPassword(registerDto.Password);

            var account = new Account
            (
                user.Id,
                login,
                passwordHash,
                email,
                registerDto.PhoneNumber,
                role
            );

            account.CreateCart();

            await _accountRepo.AddAsync(account);
            await _accountRepo.SaveChangesAsync();

            var token = _tokenGenerator.GenerateToken
            (
                accountId: account.Id,
                userId: account.UserId,
                email: account.Email,
                fullName: user.Name,
                role: role
            );

            return new AuthResultDto
            (
                token,
                account.Id,
                account.Email,
                user.Name,
                role
            );
        }
    }
}
