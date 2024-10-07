using AutoMapper;
using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Service.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitaraKauppa.Core.Users;
using Microsoft.Extensions.Configuration;
using KitaraKauppa.Service.Repositories.Cities;
using KitaraKauppa.Service.Cryptography;
using KitaraKauppa.Service.UsersService.Dtos;

namespace KitaraKauppa.Service.UsersService
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public UserManagement(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            ICityRepository cityRepository,
            IMapper mapper,
            IConfiguration configuration,
            IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _configuration = configuration;
            this._passwordHasher = passwordHasher;
        }

        public UserManagement(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ActiveInactiveUser(Guid userId, bool status)
        {

            var user = await _userRepository.GetByIdAsync(userId) ?? throw new RecordNotFoundException(typeof(User).Name);

            user.IsUserActive = status;
            await _userRepository.UpdateAsync(user);
        }


        public async Task<UserDto> CreateUser(Dtos.CreateUpdateUserDto user)
        {
            if (await _userRepository.CheckUserExistsByEmail(user.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);

            var adminUserRole = await GetAdminRole();

            var userEntity = _mapper.Map<User>(user);
            //userEntity.Id = Guid.Empty;
            userEntity.UserRoleId = adminUserRole.Id;
            userEntity.IsUserActive = true;

            var createdUser = await _userRepository.AddAsync(userEntity);

            //TODO: Send email to user with password

            return _mapper.Map<UserDto>(createdUser);
        }

            public async Task<CreateDetailedUserResponseDto> CreateProfile(CreateDetailedUserDto profile)
            {
                if (await _userRepository.CheckUserExistsByEmail(profile.Email!)) throw new RecordAlreadyExistsException(typeof(User).Name);

                if (!(await _cityRepository.IsCityExists(profile.CityId))) throw new RecordNotFoundException(typeof(City).Name);

                var userRole = await GetUserRole();

                var userEntity = _mapper.Map<User>(profile);

                userEntity.UserRoleId = userRole.Id;



                userEntity.UserCredential = new UserCredential() 
                {
                    UserName = profile.UserName,
                    Password = _passwordHasher.HashPassword(profile.Password)
                };

                var savedUser = await _userRepository.AddAsync(userEntity);

                var returnProfile = _mapper.Map<CreateDetailedUserResponseDto>(profile);
                returnProfile.UserId = savedUser.Id;
                returnProfile.Password = String.Empty;

                return returnProfile;
            }

            public void DeleteUser(Guid userId)
            {
                throw new NotImplementedException();
            }

            public async Task<UserDetailedDto> GetUser(Guid userId)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                return user is null ? throw new RecordNotFoundException("User") : _mapper.Map<UserDetailedDto>(user);
            }

            public async Task<List<UserDto>> GetUsers(UsersQueryOptions query)
            {
                var users = await _userRepository.GetUsers(query);
                return _mapper.Map<List<UserDto>>(users);
            }

            public async Task UpdateUser(Guid userId, CreateUpdateUserDto user) 
            {
                var existing = await _userRepository.GetByIdAsync(userId) ?? throw new RecordNotFoundException(typeof(User).Name);
            
                if (await _userRepository.CheckEmailExistsWithOtherUsers(userId, user.Email!)) throw new RecordAlreadyExistsException("Email");

                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.Email = user.Email;
                
                await _userRepository.UpdateAsync(existing);
            }

        private async Task<UserRole> GetAdminRole()
            {
                var adminRoleName = _configuration.GetSection("UserRoles").GetSection("Admin").Value;

                if (string.IsNullOrEmpty(adminRoleName))
                {
                    throw new RecordNotFoundException("Admin Role in config");
                }

                return await GetRole(adminRoleName);
            }

            private async Task<UserRole> GetUserRole()
            {
                var adminRoleName = _configuration.GetSection("UserRoles").GetSection("User").Value;
                if (string.IsNullOrEmpty(adminRoleName))
                {
                    throw new RecordNotFoundException("User Role in config");
                }

                return await GetRole(adminRoleName);
            }

            private async Task<UserRole> GetRole(string roleName)
            {
                var userRole = await _userRoleRepository.GetUserRoleByName(roleName);
                return userRole is null ? throw new RecordNotFoundException("User Role") : userRole;
            }
    }
    }

