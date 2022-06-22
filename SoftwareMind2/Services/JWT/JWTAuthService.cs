using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SoftwareMind.Models;
using SoftwareMind2.DTOs.Auth;
using SoftwareMind2.JWT;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.JWT
{
    public class JWTAuthService : IJWTAuthService
    {
        private const string EXPIRYTIME = "3600";
        private readonly BookingDbContext _context;
        private readonly IConfiguration _configuration;

        public JWTAuthService(BookingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task DeleteRefreshToken(int idUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.idUser == idUser);
            if (user == null) throw new Exception("No such account");
            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _context.SaveChangesAsync();
        }

        public async Task<JWTLoginResponse> Login(JWTLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.login.ToLower() == request.login);
            if (user == null) throw new Exception("No such user");
            if (request.password != user.password) throw new Exception("Wrong password");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = TokenGenerator.GenerateToken(user.idUser, user.Role, user.firstName, user.lastName, credentials);
            var refreshToken = RefreshTokenGenerator.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddDays(2);
            await _context.SaveChangesAsync();

            return new JWTLoginResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                RefreshToken = refreshToken,
                Role = user.Role,
                expiresIn = EXPIRYTIME
            };
        }

        public async Task<JWTRefreshTokenResponse> RefreshToken(JWTRefreshTokenRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.RefreshToken == request.RefreshToken);
            if (user == null) throw new Exception();
            if (user.RefreshTokenExpiry < DateTime.Now) throw new Exception();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = TokenGenerator.GenerateToken(user.idUser, user.Role, user.firstName, user.lastName, creds);

            await _context.SaveChangesAsync();

            return new JWTRefreshTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                RefreshToken = user.RefreshToken,
                Role = user.Role,
                expiresIn = EXPIRYTIME
            };
        }
    }
}
