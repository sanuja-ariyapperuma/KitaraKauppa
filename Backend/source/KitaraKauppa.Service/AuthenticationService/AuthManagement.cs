using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.Cryptography;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Repositories.InMemory;
using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.AuthenticationService
{
    public class AuthManagement : IAuthManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtManagement _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthManagement(IUserRepository userRepository, IJwtManagement jwtService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }
        public async Task<KKResult<AuthenticationResultDto>> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null || !_passwordHasher.VerifyPassword(user.UserCredential.Password, password))
            {
                return new KKResult<AuthenticationResultDto>().Fail("Username or password error");
            }

            var token = _jwtService.GenerateToken(user);
            var fullName = user.FirstName + " " + user.LastName;
            var isAdmin = user.UserRole.UserRoleName == "Admin";

            return new KKResult<AuthenticationResultDto>().SucceededWithValue(
                new AuthenticationResultDto { IsAuthenticated = true, Token = token, FullName = fullName, IsAdmin = isAdmin });
        }


        public KKResult<string> Logout(string token)
        {
            _jwtService.InvalidateToken(token);

            return new KKResult<string>().SucceededWithValue("Successfully logged out");
        }


    }
}
