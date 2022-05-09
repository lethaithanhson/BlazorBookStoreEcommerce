using BlazorEcommerce.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(
                new User
                {
                    Email = request.Email,
                    Status = AccountStatus.Unlock
                },
                request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] UserChangePassword request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), request.OldPassword, request.NewPassword);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet, Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var response = await _authService.GetUsers();
            return Ok(response);
        }
        [HttpPut("{userId}/lockorunlock"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> LockOrUnlock(int userId){
            var response = await _authService.LockOrUnclock(userId);
            return Ok(response);
        }

        [HttpPut("{userId}/userrole"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> UserRole(int userId, [FromBody] UserResponse userResponse)
        {
            var response = await _authService.UserRole(userId, userResponse);
            return Ok(response);
        }
    }
}
