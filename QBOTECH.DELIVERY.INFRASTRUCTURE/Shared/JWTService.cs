using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Shared
{
    public class JWTService : IJWTService
    {
        public JWTSettings _settings { get; }
        public JWTService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateJWToken(Users user)
        {
            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(sc);

            var claims = new[] {
                    new Claim(ClaimTypes.Name, (user.FullName)),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Role, user.Type == "A" ? "Admin": "User"),
                    new Claim("UserId",user.Id.ToString()),
                };

            var payload = new JwtPayload(
                            _settings.Issuer
                            , _settings.Audience
                            , claims
                            , DateTime.UtcNow
                            , DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes));

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
