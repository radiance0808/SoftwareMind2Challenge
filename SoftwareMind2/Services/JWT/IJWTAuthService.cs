using SoftwareMind2.DTOs.Auth;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.JWT
{
    public interface IJWTAuthService
    {
        Task<JWTLoginResponse> Login(JWTLoginRequest request);
        Task<JWTRefreshTokenResponse> RefreshToken(JWTRefreshTokenRequest request);
        Task DeleteRefreshToken(int idUser);
    }
}
