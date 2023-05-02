using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Infrastructure.Services;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthentication _authenticationManager;

        public AuthenticateController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IAuthentication authenticationManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [Route("/api/authenticate/register")]
        public async Task<ResponseDTO> Register([FromBody] UserRegisterRequestDTO model)
        {
            var result = await _authenticationManager.Register(model);
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/authenticate/login")]
        public async Task<ResponseDTO> Login([FromBody] UserLoginRequestDTO user)
        {
            var result = await _authenticationManager.Login(user);
            return result;
        }

        [HttpGet]
        [Route("/api/authenticate/getUserDetails")]
        public async Task<IEnumerable<UserDetailsDTO>> GetUserDetails()
        {
            var result = await _authenticationManager.GetUserDetails();
            return result;
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new ResponseDTO()
                {
                    Message = "Password reset successful!",
                    Status = "Success"
                });
            }

            return BadRequest(new ResponseDTO()
            {
                Message = "Password reset failed! Please check reset details and try again.",
                Status = "Error"
            });
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO request)
        {
            var response = await _authenticationManager.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);

            if (response.Status == "Success")
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{userId}/roles")]
        public async Task<ActionResult<List<string>>> GetUserRoles(string userId)
        {
            var roles = await _authenticationManager.GetUserRoles(userId);
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }


        [Authorize]
        [HttpGet("/api/authenticate/profile")]
        public async Task<ActionResult<UserResponse>> GetProfileAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,

            };
            return Ok(userResponse);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDetails(string id, UserDetailsDTO model)
        {
            // Get the user ID from the authenticated user's claims
            

            

            var result = await _authenticationManager.UpdateUserDetails(id, model);

            if (result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("api/authenticate/logout")]

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _authenticationManager.DeleteUser(id);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

    }
}
