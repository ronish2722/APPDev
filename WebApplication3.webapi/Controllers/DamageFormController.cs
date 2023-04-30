using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageFormController : ControllerBase
    {
        private readonly IDamageForm _damageForm;

        public DamageFormController(IDamageForm damageForm)
        {
            _damageForm = damageForm;
        }



        [HttpPost("Create")]
        public async Task<DamageForm> AddDamageFormDetails(DamageFromDTO damageForm)
        {
            var data = await _damageForm.AddDamageFormDetails(damageForm);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<DamageForm> GetDamageForm(int id)
        {
            var damageForm = await _damageForm.GetDamageForm(id);
            if (damageForm == null)
            {
                return null;
            }
            return damageForm;
        }

        [HttpGet("GetAllDamagForms")]
        public async Task<ActionResult<List<DamageForm>>> GetAllDamageForm()
        {
            var damageForms = await _damageForm.GetAllDamageForm();
            return Ok(damageForms);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDamageFormAsync(int id, [FromBody] DamageFromDTO damageFormDto)
        {
            if (damageFormDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updateddamageForm = await _damageForm.UpdateDamageFormAsync(id, damageFormDto);
                return Ok(updateddamageForm);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDamageFormAsync(int id)
        {
            try
            {
                await _damageForm.DeleteDamageFormAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
