using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffControllers : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStaff _staffManager;

        public StaffControllers(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IStaff staffManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _staffManager = staffManager;
        }

        [HttpPost]
        [Route("/api/authenticate/addStaff")]
        public async Task<ResponseDTO> AddStaff([FromBody] StaffDTO model)
        {
            var result = await _staffManager.AddStaff(model);
            return result;
        }

        [HttpGet]
        [Route("/api/authenticate/getStaffDetails")]
        public async Task<IEnumerable<StaffDTO>> GetStaffDetails()
        {
            var result = await _staffManager.GetStaffDetails();
            return result;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaffDetails(string id, StaffDTO model)
        {
            // Get the user ID from the authenticated user's claims




            var result = await _staffManager.UpdateStaffDetails(id, model);

            if (result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
