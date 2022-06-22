using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.Auth
{
    public class JWTRefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
