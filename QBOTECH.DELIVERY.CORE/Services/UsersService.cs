using Microsoft.Extensions.Configuration;
using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.CORE.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<Users> _userRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtService;

        public UsersService(IRepository<Users> userRepository, IUsersRepository usersRepository, IConfiguration configuration, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<UsersListDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            //Convert to List UsersListDTO
            var usersListDTO = users
                                .Select(u => new UsersListDTO
                                {
                                    Id = u.Id
                                ,

                                    FullName = u.FullName
                                ,
                                    Email = u.Email
                                ,
                                    CountryCode = u.CountryCode
                                ,
                                    PhoneNumber = u.PhoneNumber
                                }).ToList();
            return usersListDTO;
        }

        public async Task<UsersListDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            //Conver to UsersListDTO
            var userListDTO = new UsersListDTO
            {
                Id = user.Id,
                FullName = user.FullName
                                ,
                Email = user.Email
                                ,
                CountryCode = user.CountryCode
                                ,
                PhoneNumber = user.PhoneNumber
            };
            return userListDTO;
        }

        public async Task<UsersCreateResponseDTO> AddUserAsync(UsersCreateDTO usersCreateDTO)
        {
            //Convert to Users
            var user = new Users
            {
                FullName = usersCreateDTO.FullName,
                Email = usersCreateDTO.Email,
                PasswordHash = usersCreateDTO.PasswordHash,
                CountryCode = usersCreateDTO.CountryCode,
                PhoneNumber = usersCreateDTO.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            //Convert to UsersCreateResponseDTO
            var usersCreateResponseDTO = new UsersCreateResponseDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CountryCode = user.CountryCode,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };
            return usersCreateResponseDTO;
        }

        public void UpdateUser(UsersUpdateDTO usersUpdateDTO)
        {
            //Convert to Users
            var user = new Users
            {
                Id = usersUpdateDTO.Id,
                FullName = usersUpdateDTO.FullName,
                Email = usersUpdateDTO.Email,
                PasswordHash = usersUpdateDTO.PasswordHash,
                CountryCode = usersUpdateDTO.CountryCode,
                PhoneNumber = usersUpdateDTO.PhoneNumber
            };
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            _userRepository.Delete(user);
            _userRepository.SaveChanges();
        }

        //SignIn with email and password, returns user and JWT
        public async Task<UsersResponseDTO> SignInWithJwtAsync(string email, string password)
        {
            var user = await _usersRepository.SignIn(email, password);
            //Validate if user is null
            if (user == null)
                return null;

            var usersResponseDTO = new UsersResponseDTO
            {
                Id =  user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CountryCode = user.CountryCode,
                PhoneNumber = user.PhoneNumber,
                Token = _jwtService.GenerateJWToken(user)
            };
            if (usersResponseDTO == null)
                return null;

            return usersResponseDTO;
        }
    }
}
