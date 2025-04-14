using QBOTECH.DELIVERY.CORE.DTOs;

namespace QBOTECH.DELIVERY.CORE.Services
{
    public interface IUsersService
    {
        Task<UsersCreateResponseDTO> AddUserAsync(UsersCreateDTO usersCreateDTO);
        void DeleteUser(int id);
        Task<IEnumerable<UsersListDTO>> GetAllUsersAsync();
        Task<UsersListDTO> GetUserByIdAsync(int id);
        void UpdateUser(UsersUpdateDTO usersUpdateDTO);
        Task<UsersListDTO> SignInAsync(string email, string password);
    }
}