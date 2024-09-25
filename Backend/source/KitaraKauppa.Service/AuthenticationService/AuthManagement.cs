using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.Cryptography;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Repositories.InMemory;
using KitaraKauppa.Service.Repositories.Users;
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
        public async Task<AuthenticationResultDto> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return new AuthenticationResultDto() { IsAuthenticated = false };
            }

            if (_passwordHasher.VerifyPassword(user.UserCredential.Password, password))
            {
                var token = _jwtService.GenerateToken(user);
                var existingUser = await _userRepository.GetUserByUsername(username);

                var fullName = existingUser!.FirstName + " " + existingUser.LastName;
                var isAdmin = existingUser.UserRole.UserRoleName == "Admin";

                return new AuthenticationResultDto { IsAuthenticated = true, Token = token, FullName = fullName, IsAdmin = isAdmin };
            }

            return new AuthenticationResultDto() { IsAuthenticated = false };



        }

        public void Logout(string token)
        {
            _jwtService.InvalidateToken(token);
        }


    }
}
