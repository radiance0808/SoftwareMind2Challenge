using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareMind2.DTOs;
using SoftwareMind2.DTOs.Auth;
using SoftwareMind2.Services.JWT;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoftwareMind2.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IJWTAuthService _authService;
        public AuthController(IJWTAuthService authService = null)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(JWTLoginRequest request)
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken(JWTRefreshTokenRequest request)
        {
            var response = await _authService.RefreshToken(request);
            return Ok(response);
        }

        [HttpPut("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRefreshToken(UserIdRequest request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (nameIdentifier != request.idUser)
            {
                return Forbid();
            }
            await _authService.DeleteRefreshToken(request.idUser);
            return Ok();
        }
    }
}
