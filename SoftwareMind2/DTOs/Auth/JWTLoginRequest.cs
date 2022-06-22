using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.Auth
{
    public class JWTLoginRequest
    {
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
    }
}
