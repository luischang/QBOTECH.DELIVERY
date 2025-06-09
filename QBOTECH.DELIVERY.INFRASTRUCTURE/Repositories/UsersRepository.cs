using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(QbotechDeliveryContext context) : base(context)
        {
        }

        public async Task<Users> SignIn(string email, string password)
        {
            return await _dbSet
                .Where(d => d.Email == email && d.PasswordHash == password)
                .FirstOrDefaultAsync();
        }
    }
}
