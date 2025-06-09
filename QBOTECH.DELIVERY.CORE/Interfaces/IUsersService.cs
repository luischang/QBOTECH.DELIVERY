using QBOTECH.DELIVERY.CORE.DTOs;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IUsersService
    {
        Task<UsersCreateResponseDTO> AddUserAsync(UsersCreateDTO usersCreateDTO);
        void DeleteUser(int id);
        Task<IEnumerable<UsersListDTO>> GetAllUsersAsync();
        Task<UsersListDTO> GetUserByIdAsync(int id);
        Task<UsersResponseDTO> SignInWithJwtAsync(string email, string password);
        void UpdateUser(UsersUpdateDTO usersUpdateDTO);
    }
}