using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
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

        //[HttpGet("GetRequestsByUser/")]
        //public async Task<ActionResult<List<Request>>> GetRequestByUser(string userId)
        //{
        //    var request = await _requestService.GetRequestByUser(userId);
        //    return Ok(request);
        //}

        [HttpGet("GetRequestsByUser/{userId}")]
        public async Task<IActionResult> GetRequestByUser(string userId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var requestDTOs = await _requestService.GetRequestByUser(userId, fromDate, toDate);
            if (requestDTOs == null || requestDTOs.Count == 0)
            {
                return NotFound();
            }
            return Ok(requestDTOs);
        }

        [HttpGet("GetRequestsByCar/")]
        public async Task<ActionResult<List<Request>>> GetRequestByCar(int carId)
        {
            var request = await _requestService.GetRequestByCar(carId);
            return Ok(request);
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

        [Authorize(Policy = "StaffOrAdmin")]
        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptRequest(int id, string approvedBy)
        {
            var success = await _requestService.AcceptRequest(id, approvedBy);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Policy = "StaffOrAdmin")]
        [HttpPost("{id}/decline")]
        public async Task<IActionResult> DeclineRequest(int id, string approvedBy)
        {
            var success = await _requestService.DeclineRequest(id, approvedBy);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Policy = "StaffOrAdmin")]
        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteRequest(int id, string approvedBy)
        {
            var success = await _requestService.CompleteRequest(id, approvedBy);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpPost("CheckInactiveUsers")]
        public async Task<IActionResult> CheckInactiveUsers()
        {
            try
            {
                await _requestService.CheckInactiveUsers();
                return Ok("Users checked successfully.");
            }
            catch (Exception ex)
            {
                // handle exception
                return StatusCode(500, ex.Message);
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
