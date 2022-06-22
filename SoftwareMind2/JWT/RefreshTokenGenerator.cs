using System;

namespace SoftwareMind2.JWT
{
    public class RefreshTokenGenerator
    {
        public static string GenerateRefreshToken() => Guid.NewGuid().ToString();
    }
}
