using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Settings;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(Users user);
    }
}