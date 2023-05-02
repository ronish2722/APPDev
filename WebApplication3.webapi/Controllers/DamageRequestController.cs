using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageRequestController: ControllerBase
    {
        private readonly IDamageRequest _damageRequest;

        public DamageRequestController(IDamageRequest damageRequest)
        {
            _damageRequest = damageRequest;
        }


        [Authorize(Policy = "User")]
        [HttpPost("Create")]
        public async Task<DamageNotice> AddDamageRequestDetails(DamageRequestDTO damageRequest)
        {
            var data = await _damageRequest.AddDamageRequestDetails(damageRequest);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<DamageNotice> GetDamageRequest(int id)
        {
            var damageRequest = await _damageRequest.GetDamageRequest(id);
            if (damageRequest == null)
            {
                return null;
            }
            return damageRequest;
        }

        [HttpGet("GetAllDamagRequests")]
        public async Task<ActionResult<List<DamageNotice>>> GetAllDamageRequest()
        {
            var damageRequests = await _damageRequest.GetAllDamageRequest();
            return Ok(damageRequests);
            
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateDamageRequestAsync(int id, [FromBody] DamageRequestDTO damageRequestDto)
        {
            if (damageRequestDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updateddamageRequest = await _damageRequest.UpdateDamageRequestAsync(id, damageRequestDto);
                return Ok(updateddamageRequest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDamageRequestAsync(int id)
        {
            try
            {
                await _damageRequest.DeleteDamageRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
