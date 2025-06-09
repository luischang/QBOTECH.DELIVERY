using QBOTECH.DELIVERY.CORE.Entities;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> SignIn(string email, string password);
    }
}