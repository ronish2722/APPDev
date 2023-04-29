using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestContoller: ControllerBase
    {
        private readonly IRequest _requestService;

        public RequestContoller(IRequest requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<Request> CreateRequestAsync(RentRequestDTO requestDto)
        {
            var data = await _requestService.CreateRequestAsync(requestDto);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<Request> GetRequest(int id)
        {
            var request = await _requestService.GetRequest(id);
            if (request == null)
            {
                return null;
            }
            return request;
        }

        [HttpGet]
        public async Task<ActionResult<List<Request>>> GetAllRequest()
        {
            var request = await _requestService.GetAllRequest();
            return Ok(request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] RentRequestDTO requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedRequest = await _requestService.UpdateRequestAsync(id, requestDto);
                return Ok(updatedRequest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                await _requestService.DeleteRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
