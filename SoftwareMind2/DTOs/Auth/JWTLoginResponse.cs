namespace SoftwareMind2.DTOs.Auth
{
    public class JWTLoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public string Role { get; set; }

        public string expiresIn { get; set; }
    }
}
